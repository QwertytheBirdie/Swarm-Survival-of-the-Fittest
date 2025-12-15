using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    private Transform targetPlayer;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Important: prevent physics from spinning them
        rb.freezeRotation = true;
    }

    void Update()
    {
        FindClosestPlayer();
    }

    void FixedUpdate()
    {
        if (targetPlayer == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = ((Vector2)targetPlayer.position - rb.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        // DO NOT rotate the enemy here if you want it to stay still.
        // If you later want ONLY the sprite to rotate, rotate a child object instead.
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject p in players)
        {
            if (!p.activeInHierarchy) continue;

            float d = Vector2.SqrMagnitude((Vector2)p.transform.position - rb.position);
            if (d < closestDist)
            {
                closestDist = d;
                closest = p.transform;
            }
        }

        targetPlayer = closest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth ph = collision.collider.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                // Instant kill:
                ph.TakeDamage(9999);
            }
        }
    }
}
