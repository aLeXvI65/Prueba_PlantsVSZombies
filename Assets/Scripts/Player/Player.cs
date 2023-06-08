using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameObject plantToSpawn;

    private GameController gc;

    void Start() {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null ) {
            gc = gcObj.GetComponent<GameController>();
        }
    }

    void Update() {

    }

    public void preparePlantToSpawn(GameObject plant) {
        plantToSpawn = Instantiate(plant);
        plantToSpawn.SetActive(false);
    }

    public GameObject showOrHidePlantToSpawn(bool value, Floor floor) {
        // When draging a card over the floor, the plant must be shown as a preview over the floor.
        // When going outside the floor, the preview must not be shown.
        if (plantToSpawn != null) {
            plantToSpawn.SetActive(value);

            Plant plant = plantToSpawn.GetComponent<Plant>();
            if (plant != null) {
                plant.setFloorPlaced(floor);
            }

            return plantToSpawn;
        }
        return null;
    }

    public void setPlantToSpawnPosition(Vector3 position) {
        if (plantToSpawn != null) {
            plantToSpawn.transform.position = position;
        }
    }

    public void putPlantOnPlace(Card card) {
        // When the plant is about to be bought, the plant is put and attached to the floor.
        Plant plant = plantToSpawn.GetComponent<Plant>();
        if (plant != null) {
            if (plant.floorPlaced == null) {
                // If there is a plant in the floor, the buying is cancelled.
                Destroy(plantToSpawn);
            }
            else {
                if (gc.buyPlant(card.cost)) {
                    // If plant succeed to be bought, the plant is attached to floor and start working.
                    plant.setFloorPlantAttached();
                    plant.startPlant();
                    card.disableCard();
                }
                else {
                    // If bouy plant is not succesfull, the plant is cancelled.
                    Destroy(plantToSpawn);
                }
            }
        }

        plantToSpawn = null;
    }

}
