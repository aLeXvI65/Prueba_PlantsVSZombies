using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFrozen : Bullet {
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Zombie zombie = other.GetComponent<Zombie>();
            zombie.reduceVelocity(); // If bullet frozen hits Enemy, zombie speed it cut at half
            zombie.hurt(damage);
            Destroy(gameObject);
        }
    }
}
