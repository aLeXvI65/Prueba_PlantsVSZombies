using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    private float hp = 200;

    public void hurt(float damage) {
        hp -= damage;
        if (hp <= 0) {
            hp = 0;
            Destroy(gameObject);
        }
        // Stop harm routine in case another bullet hit the obstacle while it's already harmed, this will avoid having many routines
        // running at same time for this obstacle
        StopCoroutine(harmRoutine());
        StartCoroutine(harmRoutine());
    }

    IEnumerator harmRoutine() {
        // This routine add a simple visual aid to show when the obstacle receives damage, all materials of the object are put to be
        // lightly transparent, and after 0.2f seconds it goes back to be shown regularly
        Effects.applyAlphaToAllMaterials(gameObject, 0.5f);

        yield return new WaitForSeconds(0.2f);

        Effects.applyAlphaToAllMaterials(gameObject, 1f);

    }
}
