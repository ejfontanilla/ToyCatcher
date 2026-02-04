//using UnityEngine;

//public class GrumpyKid : MonoBehaviour
//{
//    public float moveSpeed = 3f;
//    public int penaltyScore = 5;

//    private Transform player;

//    void Start()
//    {
//        Debug.Log("Start GrumpyKid");
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//    }

//    void Update()
//    {
//        if (player == null) return;

//        Vector3 direction = (player.position - transform.position).normalized;
//        transform.position += direction * moveSpeed * Time.deltaTime;

//        if (transform.position.x < -10f || transform.position.x > 10f)
//        {
//            Destroy(gameObject);
//            Debug.Log("Update Destroy GrumpyKid: " + transform.position.x);
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            GameManager.Instance.AddScore(-penaltyScore);
//            Destroy(gameObject);
//            Debug.Log("OnTriggerEnter2D Destroy GrumpyKid: ");
//        }
//    }

//    void LateUpdate()
//    {
//        transform.position = new Vector3(
//            transform.position.x,
//            -1.5f,   // SAME Y as your ground
//            transform.position.z
//        );
//    }
//}

using UnityEngine;

public class GrumpyKid : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private int direction; // -1 = left, +1 = right

    public void Init(int moveDirection)
    {
        direction = moveDirection;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, 0f);
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
                GameManager.Instance.PlayerHit();
            }
        }
    }
}

