using UnityEngine;

public enum ToyType
{
    Ambulance,
    FireTruck,
    Police,
    Spider
}

public class Toy : MonoBehaviour
{
    [SerializeField] private float destroyYPosition = -6f;
    public ToyType toyType;
    public int scoreValue = 1;

    void Update()
    {
        // Destroy the toy if it falls below the screen
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
