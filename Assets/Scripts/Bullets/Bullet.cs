using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private Rigidbody rb;

    [SerializeField]
    private Vector3 force;

    public float damage;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force);
    }

    void Update() {
        // If the bullet goes over a limitX, it's destroyed, to avoid attacking zombies too far away.
        if (transform.position.x >= 4.25f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Zombie zombie = other.GetComponent<Zombie>();
            zombie.hurt(damage);
            Destroy(gameObject);
        }
        if (other.tag == "Obstacle") {
            Obstacle obstacle = other.GetComponent<Obstacle>();
            obstacle.hurt(damage);
            Destroy(gameObject);
        }
    }
}
