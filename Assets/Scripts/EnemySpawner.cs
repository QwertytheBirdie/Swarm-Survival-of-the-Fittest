using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnOffset = 2f; // how far off-screen enemies appear
    public float difficultyRate = 0.01f; // decreases spawn interval over time
    public float enemySpeedIncrease = 0.005f;

    private Camera cam;
    private float timer = 0f;
    private float elapsedTime = 0f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }

        // Gradually increase spawn rate
        if (spawnInterval > 0.5f)
            spawnInterval -= difficultyRate * Time.deltaTime;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomOffscreenPosition();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Scale up difficulty (faster enemies)
        float extraSpeed = elapsedTime * enemySpeedIncrease;
        enemy.GetComponent<Enemy>().Speed+= extraSpeed;
    }

    Vector3 GetRandomOffscreenPosition()
    {
        // Get camera bounds in world space
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Randomly pick a side (0=left, 1=right, 2=top, 3=bottom)
        int side = Random.Range(0, 4);

        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0: // left
                spawnPos = new Vector3(cam.transform.position.x - camWidth / 2 - spawnOffset,
                                       Random.Range(cam.transform.position.y - camHeight / 2, cam.transform.position.y + camHeight / 2),
                                       0f);
                break;
            case 1: // right
                spawnPos = new Vector3(cam.transform.position.x + camWidth / 2 + spawnOffset,
                                       Random.Range(cam.transform.position.y - camHeight / 2, cam.transform.position.y + camHeight / 2),
                                       0f);
                break;
            case 2: // top
                spawnPos = new Vector3(Random.Range(cam.transform.position.x - camWidth / 2, cam.transform.position.x + camWidth / 2),
                                       cam.transform.position.y + camHeight / 2 + spawnOffset,
                                       0f);
                break;
            case 3: // bottom
                spawnPos = new Vector3(Random.Range(cam.transform.position.x - camWidth / 2, cam.transform.position.x + camWidth / 2),
                                       cam.transform.position.y - camHeight / 2 - spawnOffset,
                                       0f);
                break;
        }

        return spawnPos;
    }
}
