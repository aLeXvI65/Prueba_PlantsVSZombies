using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    // Define two types of sun, normal sun is the one tha spawn randomly in the level.
    // Sunflower sun is the one that spawns over the plant.
    enum SunType { NormalSun, SunflowerSun };

    [SerializeField]
    private SunType type; 

    private float rotationSpeed = 120f;
    private float downSpeed = 0.5f;

    private float minY = 1f; // Since sun falls from sky, we stop it when it reaches a given height.

    private int sunValue = 25;

    GameController gc;

    void Start() {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null) {
            gc = gcObj.GetComponent<GameController>();
        }

        // If sun is normal, we spawn it randomly in a given area inside the level.
        if (type == SunType.NormalSun) {
            Vector3 pos = transform.position;
            pos.x = Random.Range(-4.75f, 1.5f);
            pos.y = 5f;
            pos.z = Random.Range(-4f, 0f);
            transform.position = pos;
        }

        Destroy(gameObject, 10f); // We destroy sun after 10 seconds if user don't click on it.
    }

    void Update() {
        // If normal sun is over minY, the sun keeps falling.
        if (transform.position.y > minY && type == SunType.NormalSun) {
            Vector3 pos = transform.position;
            pos.y -= downSpeed * Time.deltaTime;
            transform.position = pos;
        }

        // Rotate the sun, just for fun.
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    private void OnMouseDown() {
        if (gc != null) {
            // If sun is clicked, add sun score to game controller score.
            gc.addSunScore(sunValue);
            Destroy(gameObject);
        }
    }
}
