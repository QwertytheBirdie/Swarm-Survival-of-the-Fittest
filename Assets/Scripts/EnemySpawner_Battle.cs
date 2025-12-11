using UnityEngine;

public class EnemySpawner_Battle : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Timing")]
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 0.3f;
    public float difficultyStepTime = 30f;
    public float spawnRateStep = 0.2f;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private float currentSpawnRate;
    private float nextSpawnTime;
    private float nextDifficultyTime;

    // NEW — allows TwoPlayerGameManager to stop the spawner
    private bool spawningEnabled = true;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
        nextDifficultyTime = Time.time + difficultyStepTime;
    }

    void Update()
    {
        if (!spawningEnabled) return;    // NEW: stops everything immediately

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + currentSpawnRate;
        }

        if (Time.time >= nextDifficultyTime)
        {
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateStep);
            nextDifficultyTime = Time.time + difficultyStepTime;
        }
    }

    void SpawnEnemy()
    {
        if (!spawningEnabled) return;   // Prevent spawns mid-frame
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int idx = Random.Range(0, spawnPoints.Length);
        Transform sp = spawnPoints[idx];

        Instantiate(enemyPrefab, sp.position, Quaternion.identity);
    }

    // NEW — This is required by TwoPlayerGameManager
    public void StopSpawning()
    {
        spawningEnabled = false;
    }
}
