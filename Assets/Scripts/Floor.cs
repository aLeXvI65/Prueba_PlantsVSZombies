using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    // This bool is set true if the plant is bought and confirmed to be over the floor
    public bool hasPlantAttached = false;

    // This bool is set true if a plant is shown over the floor, the plant hasn't been bought yet.
    bool hasPlant = false;

    Player player;

    GameObject plantAttached;

    void Start() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            player = playerObj.GetComponent<Player>();
        }
    }

    void Update() {

    }

    private void OnMouseOver() {
        // If floor has already a plant attached to it, we don't want to allow adding more plants.
        if (hasPlantAttached) {
            return;
        }
        // If floor hasn't plant attached, a preview of the plant should be shown before buying it.
        if (player != null && !hasPlant) {
            // Set the plant position over the floor to be shown before buying.
            player.setPlantToSpawnPosition(transform.position + new Vector3(0, 0.75f, 0));
            plantAttached = player.showOrHidePlantToSpawn(true, this);
            hasPlant = true;
        }
    }

    private void OnMouseExit() {
        // If there is already a plant attached to floor, we just don't want to show any other plant.
        if (hasPlantAttached) {
            return;
        }
        // When mouse is out of the floor cube, the preview plant being shown will be removed
        if (player != null && hasPlant) {
            player.showOrHidePlantToSpawn(false, null);
            plantAttached = null;
            hasPlant = false;
        }
    }

    public void dettachPlant() {
        hasPlantAttached = false;
        hasPlant = false;
    }
}
