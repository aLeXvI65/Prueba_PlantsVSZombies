using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMichaelJackson : Zombie {

    [SerializeField]
    private GameObject zombieDancer; // Zombie Michael Jackson spawn 4 zombies in all directions

    private float spawnZombieDancerTime = 25;

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

        StartCoroutine(spawnZombieDancersRoutine());
    }

    void Update() {
        if (state == ZombieState.Moving) {
            Vector3 pos = transform.position;
            pos.x += vel * Time.deltaTime;
            transform.position = pos;
        }

        if (transform.position.x < loseLimitX) {
            // If zombie reach the garden, player lose the game
            gc.lose();
        }
    }

    IEnumerator spawnZombieDancersRoutine() {
        Vector3 pos;
        while (true) {
            // This spawn 4 zombies in all directions, unless one zombie could go ouside the field.
            yield return new WaitForSeconds(spawnZombieDancerTime);

            // The zombie dancer is spawned in a given position and buried on the floor.
            pos = transform.position + new Vector3(-1, -0.6f, 0);
            Instantiate(zombieDancer, pos, Quaternion.identity);

            pos = transform.position + new Vector3(1, -0.6f, 0);
            Instantiate(zombieDancer, pos, Quaternion.identity);

            gc.addZombieCount(2);

            // Avoid putting zombie dancer outside the field
            if (transform.position.z > -4f) {
                pos = transform.position + new Vector3(0, -0.6f, -1);
                Instantiate(zombieDancer, pos, Quaternion.identity);
                gc.addZombieCount(1);
            }

            // Avoid putting zombie dancer outside the field
            if (transform.position.z < 0f) {
                pos = transform.position + new Vector3(0, -0.6f, 1);
                Instantiate(zombieDancer, pos, Quaternion.identity);
                gc.addZombieCount(1);
            }
        }
    }
}
