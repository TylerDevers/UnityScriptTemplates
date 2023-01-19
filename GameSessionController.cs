using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
