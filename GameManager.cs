using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public string PlayerName, HighestScorer;
    public int HighScore = 0;

    private void Awake() {
        // singleton, ensures only one instance exists.
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start() {
        LoadHighScore();
    }

    
    [System.Serializable] // tagged for saving via json
    class SaveData
    {
        // list all desired variables to be saved.
        public string HighestScorer;
        public int HighScore;
    }

    public void SaveHighScore() {
        
        SaveData data = new SaveData();
        data.HighestScorer = HighestScorer;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

        public void LoadHighScore() {
            string path = Application.persistentDataPath + "/savefile.json";

            if (File.Exists(path)) {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                HighestScorer = data.HighestScorer;
                HighScore = data.HighScore;
            }
        }
}
