using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class UIHandler : MonoBehaviour
{
    public InputField inputField;
    public Text text, ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "High Score: " + GameManager.Instance.HighestScorer + " : " + GameManager.Instance.HighScore;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetNameInput() {
        GameManager.Instance.PlayerName = inputField.text;
        text.text = GameManager.Instance.PlayerName;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
