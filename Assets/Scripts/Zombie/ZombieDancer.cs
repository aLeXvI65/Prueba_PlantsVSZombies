using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDancer : Zombie {

    private float emergeVel = 0.5f;

    void Start() {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null) {
            gc = gcObj.GetComponent<GameController>();
        }

        initialVel = vel;

        state = ZombieState.Emerging; // Zombie Dancer comes out from the floor when spawned.
    }

    void Update() {
        if (state == ZombieState.Emerging) {
            // Move zombie in Y axis to make it emerge from the floor.
            Vector3 pos = transform.position;
            pos.y += emergeVel * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, -1, 0.75f);
            transform.position = pos;

            if (pos.y >= 0.75f) {
                // When finish emerging, zombie proceed to move.
                state = ZombieState.Moving;
            }
        }

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
}
