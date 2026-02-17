using UnityEngine;

public enum ToyType
{
    GreenDinosaur,
    FireTruck,
    PinkBunny
}

public class Toy : MonoBehaviour
{
    [SerializeField] private float destroyYPosition = -6f;
    public ToyType toyType;
    public int scoreValue = 1;
    public ParticleSystem collectStickerFX;

    void Update()
    {
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

            if (collectStickerFX != null)
            {
                ParticleSystem fx = Instantiate(
                    collectStickerFX,
                    transform.position,
                    Quaternion.identity
                );

                Destroy(fx.gameObject, 1.5f);
            }

            Destroy(gameObject);
        }
    }

}
