using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    [Header("Ownership")]
    // 0 = Enemy bullet
    // 1 = Player 1 bullet
    // 2 = Player 2 bullet
    public int shooterID = 0;

    private void Start()
    {
        // Ensure bullet dies even if it never hits anything
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move forward in local X direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // -------------------------------------------------
        // HIT ENEMY (players can kill enemies)
        // -------------------------------------------------
        if (col.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, shooterID);
            }

            Destroy(gameObject);
            return;
        }

        // -------------------------------------------------
        // HIT PLAYER (ONLY enemy bullets can hurt players)
        // -------------------------------------------------
        if (col.CompareTag("Player"))
        {
            // Block player bullets from hurting any player
            if (shooterID != 0)
                return;

            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // -------------------------------------------------
        // HIT WALL / ENVIRONMENT
        // -------------------------------------------------
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
