using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static int levelIndex = 0;

    public enum GameState { Playing, Lose, Win }

    [Header("Spawn Objects")]
    [SerializeField]
    private GameObject sun;

    [Header("HUD Texts")]
    [SerializeField]
    private TextMeshProUGUI sunScoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI zombiesLeftText;
    [SerializeField]
    private TextMeshProUGUI loseText;
    [SerializeField]
    private TextMeshProUGUI winText;

    [Header("Other")]
    [SerializeField]
    private List<LevelData> levelData;

    public int sunScore = 50;
    private int currentZombieIndex = 0;
    private int zombiesCount = 0;

    private float spawnSunTime = 6.0f;
    private float spawnZombieTime = 7.0f;

    public GameState gameState;

    void Start() {
        // Restore game in case is paused
        Time.timeScale = 1;

        // We set initial sunScore according to levelData retrieved
        sunScore = levelData[levelIndex].initialSunScore;
        // Set the number of zombies user must kill.
        zombiesCount = levelData[levelIndex].zombieList.Length;

        gameState = GameState.Playing;
        StartCoroutine(sunSpawnRoutine());
        StartCoroutine(zombieSpawnRoutine());
    }

    void Update() {
        // Update all texts shown in HUD
        if (sunScoreText != null) {
            sunScoreText.text = "" + sunScore;
        }
        if (levelText != null) {
            levelText.text = "LEVEL: " + levelData[levelIndex].levelName;
        }
        if (zombiesLeftText != null) {
            zombiesLeftText.text = "Zombies Left: " + zombiesCount;
        }
        if (zombiesCount <= 0) {
            win();
        }
    }

    IEnumerator sunSpawnRoutine() {
        yield return new WaitForSeconds(5.0f);
        while (true) {
            // Spawn a normal sun each given time.
            GameObject sunObj = Instantiate(sun);
            yield return new WaitForSeconds(spawnSunTime);
        }
    }

    IEnumerator zombieSpawnRoutine() {
        yield return new WaitForSeconds(8.0f);
        while (currentZombieIndex < levelData[levelIndex].zombieList.Length) {
            // We spawn the selected zombie that should be spawn according to the selected level in a zombieList which is inside
            // a ScriptableObject
            GameObject sunObj = Instantiate(
                levelData[levelIndex].zombieList[currentZombieIndex]);
            currentZombieIndex++;

            yield return new WaitForSeconds(spawnZombieTime);
        }
    }

    public void addSunScore(int score) {
        sunScore += score;
    }

    public void addZombieCount(int value) {
        zombiesCount += value;
    }

    public bool buyPlant(int plantValue) {
        // Check if a plant can be bought, if true the buying is succesful and return true, return false otherwise.
        if (sunScore >= plantValue) {
            sunScore -= plantValue;
            return true;
        }
        return false;
    }

    public void lose() {
        if (gameState == GameState.Playing) {
            gameState = GameState.Lose;
            StartCoroutine(loseRoutine());
        }        
    }

    IEnumerator loseRoutine() {
        // Pause Game and show lose text
        Time.timeScale = 0;

        if (loseText != null) {
            loseText.gameObject.SetActive(true);
        }

        // We wait 5 seconds to go MainMenu, use WaitForSecondsRealtime cause game is paused
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void win() {
        if (gameState == GameState.Playing) {
            gameState = GameState.Win;
            StartCoroutine(winRoutine());
        }
    }

    IEnumerator winRoutine() {
        // Pause Game and show win text
        Time.timeScale = 0;

        if (winText != null) {
            winText.gameObject.SetActive(true);
        }

        // We wait 5 seconds to fo MainMenu, use WaitForSecondsRealtime cause game is paused
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
