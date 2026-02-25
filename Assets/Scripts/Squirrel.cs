using UnityEngine;

public class Squirrel : MonoBehaviour
{
    public float moveSpeed = 2.5f;

    private Rigidbody2D rb;
    private int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Move toward center of screen
        direction = transform.position.x < 0 ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * moveSpeed, 0f);

        // Flip sprite based on direction
        Vector3 scale = transform.localScale; scale.x = direction == 1 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); 
        transform.localScale = scale;
    }

    void Update()
    {
        // Stop movement if game over
        if (GameManager.Instance != null && GameManager.Instance.IsGameEnded)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && !player.IsStunned)
            {
                player.React(player.admireSprite);
            }
        }
    }
}