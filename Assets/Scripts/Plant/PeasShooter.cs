using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasShooter : Plant {

    public float peaShootTime = 2.0f;

    public GameObject pea;

    public Transform muzzle; // Muzzle has the position where we want to spawn the peas.

    void Start() {
        
    }

    void Update() {
        
    }

    public override void startPlant() {
        activateColliders();
        StartCoroutine(spawnPeasRoutine());
    }

    IEnumerator spawnPeasRoutine() {
        while (true) {
            // Spawn pea bulleat each given time
            yield return new WaitForSeconds(peaShootTime);
            Instantiate(pea,muzzle.position, Quaternion.identity);
        }
    }
}
