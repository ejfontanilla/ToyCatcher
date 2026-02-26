using UnityEngine;

public class GrumpyKid : MonoBehaviour
{
    public float moveSpeed = 995f;

    private Rigidbody2D rb;
    private int direction;
    private bool isFrozen = false;
    private bool isTopLane;

    private SpriteRenderer spriteRenderer;
    private EnemySpawner spawner;

    [Header("End Game")]
    public Sprite endGameSprite;

    public void Init(int moveDirection, bool topLane)
    {
        direction = moveDirection;
        isTopLane = topLane;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        spawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        if (!GameManager.Instance.isPlaying || isFrozen)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(direction * moveSpeed, 0f);
    }

    public void Freeze()
    {
        isFrozen = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (spriteRenderer != null && endGameSprite != null)
        {
            spriteRenderer.sprite = endGameSprite;
        }
    }

    void OnBecameInvisible()
    {
        if (!isFrozen)
        {
            if (spawner != null)
            {
                spawner.NotifyEnemyDestroyed(isTopLane);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFrozen) return;

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && !player.IsStunned)
            {
                GameManager.Instance.PlayerHit();
            }
        }
    }
}