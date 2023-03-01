using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;


/*
    use to maintain game state, reset and change levels, GUI, coins, etc.
*/
public class GameSessionController : MonoBehaviour
{


    private void Awake() {
        int numberOfGameSessions = FindObjectsOfType<GameSessionController>().Length;
        if (numberOfGameSessions > 1) {
            DestroyImmediate(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }


    void ResetLevel() {
        int currentSceneIndex = SceneManagment;
    }





}
