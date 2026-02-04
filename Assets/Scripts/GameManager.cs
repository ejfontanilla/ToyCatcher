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

    public int hitPenalty = 2;
    public PlayerController player;


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
        StartGame();
    }

    void Update()
    {
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
        isPlaying = false;
        Debug.Log("Game Over! Score: " + score);
        // Game Over UI later
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        timerText.text = Mathf.Ceil(timer).ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
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
        player.Stun();
    }

}
