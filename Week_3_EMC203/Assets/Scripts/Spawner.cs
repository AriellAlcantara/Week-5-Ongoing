using UnityEngine;
using System.Collections;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesLeftText;

    public float initialSpawnDelay = 3f;
    public float spawnRate = 2f;
    public int initialEnemyCount = 10;
    public float spawnRateMultiplier = 0.9f; // Increases spawn speed per wave
    public float minSpawnRate = 0.3f;
    public int maxWaves = 10; // Winning condition

    private int currentWave = 1;
    private int enemiesToSpawn;
    private float currentSpawnRate;
    private bool waveActive = false;
    private bool gameWon = false;

    void Start()
    {
        enemiesToSpawn = initialEnemyCount;
        currentSpawnRate = spawnRate;
        UpdateWaveText();
        StartWave();
    }

    void Update()
    {
        if (gameWon) return;

        UpdateEnemiesLeftUI();

        // If all enemies are gone and wave finished spawning, start the next wave
        if (waveActive && CountEnemies() == 0)
        {
            waveActive = false;
            if (currentWave < maxWaves)
            {
                Invoke(nameof(StartNextWave), 2f);
            }
            else
            {
                WinGame();
            }
        }
    }

    public void StartWave()
    {
        waveActive = true;
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
    }

    public void StartNextWave()
    {
        currentWave++;
        enemiesToSpawn += 10; // More enemies each wave
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate * spawnRateMultiplier); // Increase spawn speed

        UpdateWaveText();
        StartWave();
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
    }

    private void UpdateEnemiesLeftUI()
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = "Enemies Left: " + CountEnemies();
        }
    }

    private int CountEnemies()
    {
        return FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
    }

    private void WinGame()
    {
        gameWon = true;
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = "You Win!";
        }
    }
}
