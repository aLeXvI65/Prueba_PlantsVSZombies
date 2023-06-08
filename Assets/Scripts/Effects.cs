using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects {
    
    // Given an object, this function takes the object and all its childs and apply transparency to all the found materials.
    // This is used to create a harm effect for plants, zombies and obstacles.
    public static void applyAlphaToAllMaterials(GameObject obj, float alpha) {
        Renderer renderer = obj.GetComponent<Renderer>();
        Color color;
        if (renderer != null) {
            color = renderer.material.color;
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);
        }

        for (int i = 0; i < obj.transform.childCount; i++) {
            renderer = obj.transform.GetChild(i).GetComponent<Renderer>();
            if (renderer != null) {
                color = renderer.material.color;
                renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            }
        }
    }

}
