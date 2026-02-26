using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    [SerializeField] private float spawnY = 4f;

    public GameObject[] toyPrefabs;
    public float spawnRate = 1.5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnToy), 1f, spawnRate);
    }

    void SpawnToy()
    {
        if (!GameManager.Instance.isPlaying) return;
        if (GameManager.Instance != null && GameManager.Instance.IsGameEnded)
            return;

        int index = Random.Range(0, toyPrefabs.Length);
        float randomX = Random.Range(-7f, 7f);

        Instantiate(
            toyPrefabs[index],
            new Vector3(randomX, spawnY, 0),
            Quaternion.identity
        );
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.toySpawnSound, 0.1f
            );
        }
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnToy));
    }
}
