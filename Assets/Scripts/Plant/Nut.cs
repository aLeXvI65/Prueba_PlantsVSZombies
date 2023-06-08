using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : Plant {
    
    // This class is too small an look useless for now, I left it thinking in an scalable context, cause in the future the nut will need code to change
    // the mesh to a hurt nut in two levels when its hp reach some thresholds. Also this match Oriented Object Programming.

    void Start() {
        
    }

    void Update() {
        
    }

    public override void startPlant() {
        activateColliders();
    }
}
