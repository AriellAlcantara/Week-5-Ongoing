using UnityEngine;
using System.Collections;
using TMPro; // Import TextMeshPro namespace

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public TextMeshProUGUI waveText; // Assign this in Inspector

    public float initialSpawnDelay = 3f;
    public float spawnRate = 2f;
    public int initialEnemyCount = 10;
    public float spawnRateDecrease = 0.2f;
    public float minSpawnRate = 0.5f;

    private int currentWave = 1;
    private int enemiesToSpawn;
    private float currentSpawnRate;

    void Start()
    {
        enemiesToSpawn = initialEnemyCount;
        currentSpawnRate = spawnRate;
        UpdateWaveText(); // Update UI at start
        StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnRate);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy.enemiesLeft++;
    }

    public void StartNextWave()
    {
        currentWave++;
        enemiesToSpawn += 10; // Increase enemies by 10
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease); // Reduce spawn delay

        UpdateWaveText(); // Update UI when wave increases
        StartWave();
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
    }
}
