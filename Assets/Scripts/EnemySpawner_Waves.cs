using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public static EnemyWaveSpawner Instance;

    [Header("Enemy Prefabs")]
    public GameObject enemyFast;
    public GameObject enemyTank;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int baseEnemies = 5;
    public float spawnDelay = 0.4f;

    private int enemiesAlive = 0;
    private bool waveActive = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartWave(int waveNumber)
    {
        Debug.Log($"[SPAWNER] Starting Wave {waveNumber}");
        StopAllCoroutines();
        StartCoroutine(SpawnWaveRoutine(waveNumber));
    }

    private IEnumerator SpawnWaveRoutine(int wave)
    {
        enemiesAlive = 0;
        waveActive = true;

        int count = baseEnemies + wave;
        Debug.Log($"[SPAWNER] Spawning {count} enemies");

        for (int i = 0; i < count; i++)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // --- NEW TANK SCALING LOGIC ---
            int tankChance = Mathf.Min(50, wave * 5); // max 50%
            bool spawnTank = Random.Range(0, 100) < tankChance;

            GameObject prefab = spawnTank ? enemyTank : enemyFast;
            // -----------------------------

            GameObject enemy = Instantiate(prefab, sp.position, Quaternion.identity);

            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.SetSpawner(this);
            }

            enemiesAlive++;


            yield return new WaitForSeconds(spawnDelay);
        }


        Debug.Log("[SPAWNER] Finished spawning enemies");

        float adjustedDelay = Mathf.Max(
           0.15f,
           spawnDelay - (wave * 0.02f)
           );

    }

    public void EnemyKilled()
    {
        if (!waveActive) return;

        enemiesAlive--;
        Debug.Log($"[SPAWNER] Enemy killed. Alive = {enemiesAlive}");

        if (enemiesAlive <= 0)
        {
            waveActive = false;
            Debug.Log("[SPAWNER] Wave complete");

            if (CoopGameManager.Instance != null)
            {
                CoopGameManager.Instance.OnWaveCompleted();
                CoopGameManager.Instance.OnWaveComplete();
                CoopGameManager.Instance.WaveCompleted();
            }
        }
    }

    // Compatibility aliases
    public void OnEnemyKilled() => EnemyKilled();
    public void NotifyEnemyKilled() => EnemyKilled();
}
