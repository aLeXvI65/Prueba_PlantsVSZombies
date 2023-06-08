using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant {

    [SerializeField]
    private GameObject sun;

    private float spawnSunTime = 6.0f;
    private float sunOffsetY = 0.9f; // offset to put sun over the plant

    void Start() {
        
    }

    void Update() {
        
    }

    public override void startPlant() {
        activateColliders();
        StartCoroutine(spawnSunRoutine());
    }

    IEnumerator spawnSunRoutine() {
        Vector3 pos = transform.position + new Vector3(0, sunOffsetY, 0);

        yield return new WaitForSeconds(5.0f);
        while (true) {
            // Spawn a sunflower sun each given time.
            GameObject sunObj = Instantiate(sun, pos, Quaternion.identity);
            yield return new WaitForSeconds(spawnSunTime);
        }
    }
}
