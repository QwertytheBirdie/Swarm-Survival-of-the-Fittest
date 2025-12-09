using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Endless : MonoBehaviour
{
    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Pattern Settings")]
    public float timeBetweenPatternSteps = 0.75f;  // delay between steps inside a pattern
    public float timeBetweenPatterns = 3f;         // delay between patterns
    public float enemyBaseSpeed = 2f;              // starting speed
    public float enemySpeedIncreasePerPattern = 0.25f;

    private int currentPatternIndex = 0;
    private float enemySpeedMultiplier = 1f;

    // ------------------------------
    // 10 PATTERNS (CUSTOMIZABLE)
    // ------------------------------
    private List<List<int[]>> patterns = new List<List<int[]>>();

    void Start()
    {
        BuildPatterns();
        StartCoroutine(RunPatterns());
    }

    void BuildPatterns()
    {
        patterns = new List<List<int[]>>
        {
            // PATTERN 1 — simple single spawns
            new List<int[]>
            {
                new int[] {1},
                new int[] {4},
                new int[] {7},
                new int[] {2}
            },

            // PATTERN 2 — 2-at-once spawns
            new List<int[]>
            {
                new int[] {1, 10},
                new int[] {3, 7},
                new int[] {2, 5},
                new int[] {8, 12}
            },

            // PATTERN 3 — mixed single + double
            new List<int[]>
            {
                new int[] {4},
                new int[] {2, 9},
                new int[] {5},
                new int[] {1, 3}
            },

            // PATTERN 4 — many small single spawns
            new List<int[]>
            {
                new int[] {7},
                new int[] {14},
                new int[] {0},
                new int[] {9},
                new int[] {3}
            },

            // PATTERN 5 — heavy double bursts
            new List<int[]>
            {
                new int[] {6, 10},
                new int[] {2, 8},
                new int[] {4, 11},
                new int[] {1, 15}
            },

            // PATTERN 6 — triple spawns (intense)
            new List<int[]>
            {
                new int[] {1, 5, 9},
                new int[] {3, 7, 12},
            },

            // PATTERN 7 — alternating 1 then 2
            new List<int[]>
            {
                new int[] {4},
                new int[] {6, 10},
                new int[] {3},
                new int[] {2, 7},
                new int[] {8}
            },

            // PATTERN 8 — widespread pressure
            new List<int[]>
            {
                new int[] {0, 7},
                new int[] {4},
                new int[] {2, 9},
                new int[] {13},
            },

            // PATTERN 9 — panic swarm
            new List<int[]>
            {
                new int[] {1, 4},
                new int[] {7, 10},
                new int[] {12, 15},
                new int[] {0, 3}
            },

            // PATTERN 10 — boss-like build-up then burst
            new List<int[]>
            {
                new int[] {2},
                new int[] {5},
                new int[] {9},
                new int[] {12},
                new int[] {1, 3, 7}    // TRIPLE BURST
            },
        };
    }

    IEnumerator RunPatterns()
    {
        while (true)
        {
            List<int[]> pattern = patterns[currentPatternIndex];

            foreach (int[] spawnGroup in pattern)
            {
                SpawnGroup(spawnGroup);
                yield return new WaitForSeconds(timeBetweenPatternSteps);
            }

            // difficulty increases every pattern
            enemySpeedMultiplier += enemySpeedIncreasePerPattern;

            // next pattern
            currentPatternIndex++;
            if (currentPatternIndex >= patterns.Count)
                currentPatternIndex = 0; // loop forever

            yield return new WaitForSeconds(timeBetweenPatterns);
        }
    }

    void SpawnGroup(int[] spawns)
    {
        foreach (int i in spawns)
        {
            if (i < 0 || i >= spawnPoints.Length)
            {
                Debug.LogWarning("Spawn index out of range: " + i);
                continue;
            }

            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);

            // Apply difficulty scaling
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
            {
                e.moveSpeed = enemyBaseSpeed + enemySpeedMultiplier;
            }
        }
    }
}
