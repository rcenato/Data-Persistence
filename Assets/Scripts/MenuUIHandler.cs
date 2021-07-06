using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    public Text playerName;
    public Text highScoreText;
    public void StartNew()
    {
        Data.Instance.playerName = playerName.text.ToString();
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        LoadScore();
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            highScoreText.text = "High Score: " + Data.Instance.highScorePlayerName + " " + Data.Instance.highScore;
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    [System.Serializable]
    class SaveData
    {
        public int score;
        public string playerName;
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Data.Instance.highScore = data.score;
            Data.Instance.highScorePlayerName = data.playerName;
        }
    }
}
