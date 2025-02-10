using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float initialSpawnDelay = 3f;
    public float spawnRate = 2f;
    public int enemiesBeforeIncrease = 15;
    public float spawnRateDecrease = 0.2f;
    public float minSpawnRate = 0.5f;

    private int enemiesSpawned = 0;
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = spawnRate;
        InvokeRepeating(nameof(SpawnEnemy), initialSpawnDelay, currentSpawnRate);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemiesSpawned++;

        if (enemiesSpawned % enemiesBeforeIncrease == 0)
        {
            IncreaseSpawnRate();
        }
    }

    void IncreaseSpawnRate()
    {
        CancelInvoke(nameof(SpawnEnemy));
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease);
        InvokeRepeating(nameof(SpawnEnemy), currentSpawnRate, currentSpawnRate);
    }
}
