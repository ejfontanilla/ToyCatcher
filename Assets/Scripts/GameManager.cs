using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 60f;
    private float timer;

    public bool isPlaying = false;

    public int score = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public Color normalTimerColor = Color.white;
    public Color warningTimerColor = Color.red;

    private bool isFlashing = false;

    public int hitPenalty = 2;
    public PlayerController player;

    public bool IsGameEnded { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = gameTime;
        timerText.color = normalTimerColor;
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }

        if (!isPlaying) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            EndGame();
        }
    }

    void StartGame()
    {
        isPlaying = true;
    }

    void EndGame()
    {
        if (IsGameEnded) return;
        IsGameEnded = true;

        SoundManager.Instance.PlaySFX(
        SoundManager.Instance.gameOverSound);

        FreezePlayer();
        FreezeAllGrumpyKids();
        StopToySpawning();
        StopTimer();

        UIManager.Instance.ShowEndGameUI();

    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        timerText.text = Mathf.Ceil(timer).ToString();
        if (timer <= 5f && !isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashTimer());
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        if (amount > 0)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.catchSound
            );
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void PlayerHit()
    {
        AddScore(-hitPenalty);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.hitSound
            );
        }

        player.Stun();
    }

    System.Collections.IEnumerator FlashTimer()
    {
        while (timer > 0f)
        {
            timerText.color = warningTimerColor;
            yield return new WaitForSeconds(0.3f);

            timerText.color = normalTimerColor;
            yield return new WaitForSeconds(0.3f);
        }
        timerText.color = warningTimerColor;
    }

    void FreezeAllGrumpyKids()
    {
        GrumpyKid[] grumpyKids = FindObjectsByType<GrumpyKid>(FindObjectsSortMode.None);

        foreach (GrumpyKid kid in grumpyKids)
        {
            kid.Freeze();
        }
    }

    void StopTimer()
    {
        isPlaying = false;
        timer = 0f;
    }

    void FreezePlayer()
    {
        if (player != null)
        {
            player.Freeze();
        }
    }

    public void Freeze()
    {
        enabled = false;
    }


    public ToySpawner toySpawner;

    void StopToySpawning()
    {
        ToySpawner[] spawners = FindObjectsByType<ToySpawner>(FindObjectsSortMode.None);

        foreach (ToySpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }

}
