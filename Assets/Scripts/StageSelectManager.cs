using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectManager : MonoBehaviour
{
    public TextMeshProUGUI daycareHighScoreText;
    public TextMeshProUGUI playgroundHighScoreText;

    void Start()
    {
        daycareHighScoreText.text = "High Score: " +
            HighScoreManager.Instance.GetHighScore("DayCareStage");

        playgroundHighScoreText.text = "High Score: " +
            HighScoreManager.Instance.GetHighScore("PlaygroundStage");
    }

    public void LoadDayCareStage()
    {
        SceneManager.LoadScene("DayCareStage");
    }

    public void LoadPlaygroundStage()
    {
        SceneManager.LoadScene("PlaygroundStage");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}