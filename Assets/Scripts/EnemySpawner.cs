using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;   // Assign your 16 spawn points here

    [Header("Pattern Settings")]
    public float spawnDelay = 1.5f;   // Time between spawns
    private int patternIndex = 0;     // Which pattern we are on
    private int spawnIndex = 0;       // Which value inside a pattern we are currently using

    // Create your patterns (each number corresponds to an index in the spawnPoints array)
    private List<int[]> patterns = new List<int[]>();

    void Start()
    {
        // EXAMPLE patterns — customize how you like
        patterns.Add(new int[] { 0, 8, 11, 15 });
        patterns.Add(new int[] { 3, 7, 12, 1, 8, 14 });
        patterns.Add(new int[] { 2, 4, 6, 9, 11, 13 });
        patterns.Add(new int[] { 15, 14, 13, 12, 11, 10, 9 });
        patterns.Add(new int[] { 1, 10, 6, 7, 9, 4, 13, 10, 11, 3 });
        patterns.Add(new int[] { 2, 5, 1, 0, 11, 4, 7, 3, 2, 0 });
        patterns.Add(new int[] { 15, 6, 10, 0, 13, 3, 9, 12, 0, 8 });
        patterns.Add(new int[] { 1, 9, 2, 0, 4, 9, 7, 10, 2, 0 });

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Get which spawn point to use
            int spawnPointID = patterns[patternIndex][spawnIndex];
            Transform point = spawnPoints[spawnPointID];

            // Spawn enemy
            Instantiate(enemyPrefab, point.position, Quaternion.identity);

            // Move to next spawn point in the pattern
            spawnIndex++;

            // If we finished current pattern → go to the next one
            if (spawnIndex >= patterns[patternIndex].Length)
            {
                spawnIndex = 0;
                patternIndex++;

                // If we reached last pattern → loop back to start
                if (patternIndex >= patterns.Count)
                    patternIndex = 0;
            }

            // Difficulty increases slowly over time
            spawnDelay = Mathf.Max(0.35f, spawnDelay - 0.01f);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
