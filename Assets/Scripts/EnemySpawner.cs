using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject grumpyKidPrefab;

    public Transform topFloor;
    public Transform bottomFloor;

    public float spawnOffsetX = 10f;

    private GameObject currentEnemy;

    void Update()
    {
        if (currentEnemy == null)
        {
            SpawnGrumpyKid();
        }
    }

    void SpawnGrumpyKid()
    {
        // Choose lane
        float laneY = Random.value > 0.5f
            ? topFloor.position.y
            : bottomFloor.position.y;

        // Choose side
        bool spawnFromLeft = Random.value > 0.5f;

        float spawnX = spawnFromLeft ? -spawnOffsetX : spawnOffsetX;
        int direction = spawnFromLeft ? 1 : -1;

        Vector3 spawnPos = new Vector3(spawnX, laneY, 0f);

        currentEnemy = Instantiate(grumpyKidPrefab, spawnPos, Quaternion.identity);

        // Set movement direction
        currentEnemy.GetComponent<GrumpyKid>().Init(direction);

        //Flip sprite based on spawn side
        Vector3 scale = currentEnemy.transform.localScale;
        scale.x = spawnFromLeft
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        currentEnemy.transform.localScale = scale;
    }
}
