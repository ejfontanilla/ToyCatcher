using UnityEngine;

public class SquirrelSpawner : MonoBehaviour
{
    public GameObject squirrelPrefab;

    public Transform topFloor;
    public Transform bottomFloor;

    public float spawnOffsetX = 10f;
    public float spawnInterval = 6f;

    private float timer;
    private GameObject currentSquirrel;

    void Update()
    {
        if (!GameManager.Instance.isPlaying) return;
        // Stop spawning if game over
        if (GameManager.Instance != null && GameManager.Instance.IsGameEnded)
            return;

        // Only spawn if there is NO squirrel
        if (currentSquirrel != null)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnSquirrel();
        }
    }

    void SpawnSquirrel()
    {
        float spawnY = Random.value > 0.5f ? topFloor.position.y : bottomFloor.position.y;
        bool spawnFromLeft = Random.value > 0.5f;
        float spawnX = spawnFromLeft ? -spawnOffsetX : spawnOffsetX;

        currentSquirrel = Instantiate(
            squirrelPrefab,
            new Vector3(spawnX, spawnY, 0f),
            Quaternion.identity
        );
    }
}