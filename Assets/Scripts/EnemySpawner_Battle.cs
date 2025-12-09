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

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
        nextDifficultyTime = Time.time + difficultyStepTime;
    }

    void Update()
    {
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
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int idx = Random.Range(0, spawnPoints.Length);
        Transform sp = spawnPoints[idx];

        Instantiate(enemyPrefab, sp.position, Quaternion.identity);
    }
}
