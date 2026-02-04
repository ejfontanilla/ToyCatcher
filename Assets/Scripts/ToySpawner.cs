using UnityEngine;

public class ToySpawner : MonoBehaviour
{
    //[SerializeField] private GameObject toyPrefab;
    //[SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnY = 4f;
    //[SerializeField] private float minX = -7f;
    //[SerializeField] private float maxX = 7f;

    public GameObject[] toyPrefabs;
    public float spawnRate = 1.5f;

    //private float timer;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnToy), 1f, spawnRate);
    }

    void SpawnToy()
    {
        int index = Random.Range(0, toyPrefabs.Length);
        float randomX = Random.Range(-7f, 7f);

        Instantiate(
            toyPrefabs[index],
            //new Vector3(randomX, transform.position.y, 0),
            new Vector3(randomX, spawnY, 0),
            Quaternion.identity
        );
    }

    //void Update()
    //{
    //    timer += Time.deltaTime;

    //    if (timer >= spawnInterval)
    //    {
    //        SpawnToy();
    //        timer = 0f;
    //    }
    //}

    //void SpawnToy()
    //{
    //    float randomX = Random.Range(minX, maxX);
    //    Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

    //    Instantiate(toyPrefab, spawnPosition, Quaternion.identity);
    //}
}
