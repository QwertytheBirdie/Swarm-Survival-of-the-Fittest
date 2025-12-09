using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    [Header("Shooter Info")]
    public int shooterID = 0;
    // 0 = enemy (if you ever have enemy bullets)
    // 1 = Player 1
    // 2 = Player 2

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb != null)
        {
            // Assuming bullet sprite faces right in local space
            rb.linearVelocity = transform.right * speed;
        }

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 1. Ignore players completely (no PvP damage)
        if (col.CompareTag("Player") || col.CompareTag("Player1") || col.CompareTag("Player2"))
            return;

        // 2. Hit enemy
        if (col.CompareTag("Enemy"))
        {
            EnemyHealth eh = col.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage, shooterID);
            }

            Destroy(gameObject);
            return;
        }

        // 3. Hit walls / bounds
        if (col.CompareTag("Wall") || col.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
