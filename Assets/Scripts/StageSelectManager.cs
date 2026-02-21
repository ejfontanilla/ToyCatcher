using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
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
