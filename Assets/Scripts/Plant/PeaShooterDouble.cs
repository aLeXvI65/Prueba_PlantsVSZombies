using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterDouble : PeasShooter {

    private float fireRate = 0.2f;

    void Start() {
        
    }

    void Update() {
        
    }

    public override void startPlant() {
        activateColliders();
        StartCoroutine(spawnPeasDoubleRoutine());
    }

    IEnumerator spawnPeasDoubleRoutine() {
        while (true) {
            // Spawn two peas with a given fireRate of separation each given time.
            yield return new WaitForSeconds(peaShootTime);
            Instantiate(pea, muzzle.position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
            Instantiate(pea, muzzle.position, Quaternion.identity);
        }
    }
}
