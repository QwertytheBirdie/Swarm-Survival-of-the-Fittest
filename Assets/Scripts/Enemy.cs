using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 3f;           // How fast the enemy moves
    private Transform player;              // Reference to the player
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player in the scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * Speed;
        }
    }

    // Called when enemy collides with player or bullet
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If hit by player bullet
        if (other.CompareTag("Bullet"))
        {
            // Increment kill count
            GameManager.instance.AddKill();

            // Destroy the bullet
            Destroy(other.gameObject);

            // Destroy this enemy
            Destroy(gameObject);
        }

        // If touches player ? Game Over
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }


}
