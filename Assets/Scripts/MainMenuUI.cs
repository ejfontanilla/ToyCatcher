using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI musicButtonText;

    void Start()
    {
        UpdateMusicButtonText();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
        UpdateMusicButtonText();
    }

    void UpdateMusicButtonText()
    {
        if (SoundManager.Instance.IsMusicEnabled)
            musicButtonText.text = "Music: ON";
        else
            musicButtonText.text = "Music: OFF";
    }
}