using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        Vector2 dir = (player.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f; // Freeze game
            Debug.Log("Game Over!");
        }
    }
}
