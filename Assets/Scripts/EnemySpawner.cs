using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject grumpyKidPrefab;

    [Header("Floors")]
    public Transform topFloor;
    public Transform bottomFloor;

    [Header("Spawn Settings")]
    public float spawnOffsetX = 10f;
    public float spawnInterval = 3f;
    public bool allowMultipleEnemies = true;
    public int maxEnemies = 2;

    private float spawnTimer;

    private int currentEnemyCount = 0;
    private bool topOccupied = false;
    private bool bottomOccupied = false;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            if (currentEnemyCount < maxEnemies)
            {
                SpawnSingleEnemy();
            }
        }
    }

    void SpawnSingleEnemy()
    {
        bool spawnTop;

        if (!topOccupied && !bottomOccupied)
        {
            spawnTop = Random.value > 0.5f;
        }
        else if (!topOccupied)
        {
            spawnTop = true;
        }
        else if (!bottomOccupied)
        {
            spawnTop = false;
        }
        else
        {
            return;
        }

        float laneY = spawnTop ? topFloor.position.y : bottomFloor.position.y;

        bool spawnFromLeft = Random.value > 0.5f;

        float spawnX = spawnFromLeft ? -spawnOffsetX : spawnOffsetX;
        int direction = spawnFromLeft ? 1 : -1;

        Vector3 spawnPos = new Vector3(spawnX, laneY, 0f);

        GameObject enemy = Instantiate(grumpyKidPrefab, spawnPos, Quaternion.identity);

        enemy.GetComponent<GrumpyKid>().Init(direction, spawnTop);

        Vector3 scale = enemy.transform.localScale;
        scale.x = spawnFromLeft
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        enemy.transform.localScale = scale;

        if (spawnTop)
            topOccupied = true;
        else
            bottomOccupied = true;

        currentEnemyCount++;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.grumpyEnterSound
            );
        }
    }

    void SpawnGrumpyKidInFreeLane()
    {
        bool spawnTop;

        if (!topOccupied && !bottomOccupied)
        {
            spawnTop = Random.value > 0.5f;
        }
        else if (!topOccupied)
        {
            spawnTop = true;
        }
        else
        {
            spawnTop = false;
        }

        float laneY = spawnTop
            ? topFloor.position.y
            : bottomFloor.position.y;

        bool spawnFromLeft = Random.value > 0.5f;

        float spawnX = spawnFromLeft ? -spawnOffsetX : spawnOffsetX;
        int direction = spawnFromLeft ? 1 : -1;

        Vector3 spawnPos = new Vector3(spawnX, laneY, 0f);

        GameObject enemy = Instantiate(grumpyKidPrefab, spawnPos, Quaternion.identity);

        enemy.GetComponent<GrumpyKid>().Init(direction, spawnTop);

        Vector3 scale = enemy.transform.localScale;
        scale.x = spawnFromLeft
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        enemy.transform.localScale = scale;

        if (spawnTop)
            topOccupied = true;
        else
            bottomOccupied = true;

        currentEnemyCount++;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.grumpyEnterSound
            );
        }
    }

    public void NotifyEnemyDestroyed(bool wasTopLane)
    {
        currentEnemyCount--;

        if (wasTopLane)
            topOccupied = false;
        else
            bottomOccupied = false;
    }
}