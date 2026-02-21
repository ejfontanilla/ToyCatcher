using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("End Game UI")]
    public GameObject endGamePanel;
    public ParticleSystem stickerFX;

    [Header("End Game Text")]
    public TextMeshProUGUI levelCompleteText;

    public GameObject gameOverPanel;
    public GameObject quitConfirmPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowEndGameUI()
    {
        // Show panel
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (levelCompleteText != null)
        {
            StartCoroutine(LevelCompletePop());
        }

        // Stickers / confetti
        if (stickerFX != null)
        {
            ParticleSystem fx = Instantiate(stickerFX);

            GameObject fxCanvasGO = GameObject.Find("FXCanvas");
            if (fxCanvasGO != null)
                fx.transform.SetParent(fxCanvasGO.transform, false);

            fx.Play();
            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    IEnumerator LevelCompletePop()
    {
        levelCompleteText.gameObject.SetActive(true);

        RectTransform rect = levelCompleteText.rectTransform;

        float duration = 0.4f;
        float elapsed = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 overshootScale = Vector3.one * 1.2f;
        Vector3 finalScale = Vector3.one;

        Color color = levelCompleteText.color;
        color.a = 0f;
        levelCompleteText.color = color;

        // POP IN
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rect.localScale = Vector3.Lerp(startScale, overshootScale, t);
            color.a = Mathf.Lerp(0f, 1f, t);
            levelCompleteText.color = color;

            yield return null;
        }

        // SETTLE BACK
        elapsed = 0f;
        duration = 0.15f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rect.localScale = Vector3.Lerp(overshootScale, finalScale, t);
            yield return null;
        }

        rect.localScale = finalScale;
    }

    public void ShowQuitConfirmation()
    {
        quitConfirmPanel.SetActive(true);
    }

    public void HideQuitConfirmation()
    {
        quitConfirmPanel.SetActive(false);
    }

}
