using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour {

    public float hp = 100;

    public Floor floorPlaced; // Reference to the floor the plant is attached to.

    void Start() {
        
    }

    void Update() {
        
    }

    public abstract void startPlant();

    public void activateColliders() {
        // Whan plant is put in floor and start playing, its colliders are activated so it can interact with zombies.
        Collider col = GetComponent<Collider>();
        if (col != null) {
            col.enabled = true;
        }
    }

    public void setFloorPlaced(Floor floor) {
        floorPlaced = floor;
    }

    public void setFloorPlantAttached() {
        if (floorPlaced == null) return;

        // When plant is bought and put in the floor, the floor is set to have the plant attached so no other plants can be added.
        Floor floor = floorPlaced.GetComponent<Floor>();
        if (floor != null) {
            floor.hasPlantAttached = true;
        }
    }

    public void hurt(float damage) {
        hp -= damage;
        if (hp <= 0) {
            hp = 0;
            floorPlaced.dettachPlant(); // If plant dies, floor get plant dettach to have other plants.
            Destroy(gameObject);
        }

        // Stop harm routine in case another zombie hit the plant while it's already harmed, this will avoid having many routines
        // running at same time for this plant
        StopCoroutine(harmRoutine());
        StartCoroutine(harmRoutine());
    }

    IEnumerator harmRoutine() {
        // This routine add a simple visual aid to show when the plant receives damage, all materials of the object are put to be
        // lightly transparent, and after 0.2f seconds it goes back to be shown regularly
        Effects.applyAlphaToAllMaterials(gameObject, 0.5f);

        yield return new WaitForSeconds(0.2f);

        Effects.applyAlphaToAllMaterials(gameObject, 1f);

    }
}
