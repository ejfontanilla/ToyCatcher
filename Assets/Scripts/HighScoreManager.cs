using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    string GetKey(string sceneName)
    {
        return sceneName + "_HighScore";
    }

    public void SaveHighScore(int currentScore)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string key = GetKey(sceneName);

        int savedScore = PlayerPrefs.GetInt(key, 0);

        if (currentScore > savedScore)
        {
            PlayerPrefs.SetInt(key, currentScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score for " + sceneName);
        }
    }

    public int GetHighScore(string sceneName)
    {
        string key = GetKey(sceneName);
        return PlayerPrefs.GetInt(key, 0);
    }
}