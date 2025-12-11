using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    public int shooterID = 0;  // 0 = enemy, 1 = Player1, 2 = Player2

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Hit enemy
        if (col.CompareTag("Enemy"))
        {
            EnemyHealth eh = col.GetComponent<EnemyHealth>();
            if (eh != null)
                eh.TakeDamage(damage, shooterID);

            Destroy(gameObject);
            return;
        }

        // Hit player
        if (col.CompareTag("Player"))
        {
            PlayerHealth ph = col.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        // Hit wall
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

