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
        float laneY = Random.value > 0.5f
            ? topFloor.position.y
            : bottomFloor.position.y;

        bool spawnFromLeft = Random.value > 0.5f;

        float spawnX = spawnFromLeft ? -spawnOffsetX : spawnOffsetX;
        int direction = spawnFromLeft ? 1 : -1;

        Vector3 spawnPos = new Vector3(spawnX, laneY, 0f);

        currentEnemy = Instantiate(grumpyKidPrefab, spawnPos, Quaternion.identity);

        currentEnemy.GetComponent<GrumpyKid>().Init(direction);

        Vector3 scale = currentEnemy.transform.localScale;
        scale.x = spawnFromLeft
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        currentEnemy.transform.localScale = scale;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(
                SoundManager.Instance.grumpyEnterSound
            );
        }
    }
}
