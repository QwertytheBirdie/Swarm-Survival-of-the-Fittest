using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    // 0 = enemy, 1 = player1, 2 = player2
    public int shooterID = 0;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // ✅ PLAYER BULLET → ENEMY
        if (col.CompareTag("Enemy") && shooterID != 0)
        {
            EnemyHealth eh = col.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage, shooterID);
            }

            Destroy(gameObject);
            return;
        }

        // ✅ ENEMY BULLET → PLAYER
        if (col.CompareTag("Player") && shooterID == 0)
        {
            PlayerHealth ph = col.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.KillPlayer();
            }

            Destroy(gameObject);
            return;
        }

        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

