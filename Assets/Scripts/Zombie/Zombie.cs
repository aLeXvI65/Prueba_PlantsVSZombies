using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    public enum ZombieState { Moving, Eating, Emerging }

    public float vel = -0.25f;
    public float initialVel;
    [SerializeField]
    private float hp = 100;
    [SerializeField]
    private float damage = 25;
    private float eatTime = 2;

    [SerializeField]
    private GameObject hat;
    [SerializeField]
    private float hatHP = 100;

    [HideInInspector]
    public float loseLimitX = -5.75f;

    // This boolean is added to avoid a bug where a zombie is hitted at same time by two bullets and make discount zombie ing GC twice.
    public bool isDead = false;

    [HideInInspector]
    public ZombieState state;

    private Plant plantToEat;

    [HideInInspector]
    public GameController gc;

    void Start() {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null) {
            gc = gcObj.GetComponent<GameController>();
        }

        initialVel = vel;

        // Put zombie in a random row position of the level.
        Vector3 pos = transform.position;
        pos.z = (int)Random.Range(-4, 0);
        transform.position = pos;
    }

    void Update() {
        if (state == ZombieState.Moving) {
            Vector3 pos = transform.position;
            pos.x += vel * Time.deltaTime;
            transform.position = pos;
        }

        if (transform.position.x < loseLimitX) {
            // If zombie reach the garden, player lose the game.
            gc.lose();
        }
    }

    public void hurt(float damage) {
        if (hat != null && hatHP > 0) {
            // If zombie has hat, hat life its hurt first.
            hatHP -= damage;
            if (hatHP <= 0) {
                hatHP = 0;
                Destroy(hat);
            }
        }
        else {
            hp -= damage;
            if (hp <= 0) {
                hp = 0;
                // isDead help avoid calling addZombieCount(-1) twice when zombie gets hit by 2 or more bullets at same time.
                if (!isDead) {
                    gc.addZombieCount(-1); // If zombie dies, zombie count from GameController is updated.
                    Destroy(gameObject);
                    isDead = true;
                }
            }
        }
        // Stop harm routine in case another bullet hit the zombie while it's already harmed, this will avoid having many routines
        // running at same time for this zombie
        StopCoroutine(harmRoutine());
        StartCoroutine(harmRoutine());
    }

    private void OnTriggerEnter(Collider other) {
        // If object zombie collides is a plant, we proceed to eat it
        if (other.tag == "Plant") {
            state = ZombieState.Eating; // Change Zombie behaviour to eat plant
            
            // Set plant to zombie so we can hurt plant in a Coroutine
            Plant plant = other.GetComponent<Plant>();
            if (plant != null) {
                plantToEat = plant;
            }

            StartCoroutine(eatPlantRoutine());
        }
    }

    IEnumerator eatPlantRoutine() {
        while (true) {
            yield return new WaitForSeconds(eatTime);

            if (plantToEat != null) {
                plantToEat.hurt(damage);
            }
            else {
                // If plantToEat is null, that means the zombie is done eating the plant and plant died.
                state = ZombieState.Moving;
                break;
            }
        }
    }

    IEnumerator harmRoutine() {
        // This routine add a simple visual aid to show when the zombie receives damage, all materials of the object are put to be
        // lightly transparent, and after 0.2f seconds it goes back to be shown regularly
        Effects.applyAlphaToAllMaterials(gameObject, 0.5f);

        yield return new WaitForSeconds(0.2f);

        Effects.applyAlphaToAllMaterials(gameObject, 1f);

    }

    public void reduceVelocity() {
        // If zombie is hit by frozen bullet, velocity is cut to half, after 5 seconds, zombie restore velocity
        StopCoroutine(reduceVelocityRoutine());
        StartCoroutine(reduceVelocityRoutine());
    }

    IEnumerator reduceVelocityRoutine() {
        vel = initialVel / 2;
        yield return new WaitForSeconds(5.0f);
        vel = initialVel;
    }
}
