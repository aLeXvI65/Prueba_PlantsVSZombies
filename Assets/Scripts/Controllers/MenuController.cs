using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    void Start() {
        
    }

    void Update() {
        
    }

    public void clickLevel(int level) {
        GameController.levelIndex = level;
        SceneManager.LoadScene("Game");
    }

    public void quitGame() {
        // If running in PC, quit game, if runs in unity editor, it stops playing game.
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
