using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "LevelData")]
public class LevelData : ScriptableObject {

    // This Scriptable Object is quiet small for now, but I created it thinking in an scalable context, thinking that in the future the
    // level will require more information like for example, the level type (morning, night, pool, roof), win condition, number of waves, etc.

    public string levelName;
    public GameObject[] zombieList;

    // This won't be fundamental for original Plants VS Zombies game, but I added to test a special level called Dark Souls where user starts with 1000 sun
    public int initialSunScore;

}
