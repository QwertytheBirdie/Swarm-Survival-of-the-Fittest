using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform targetPlayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject p in players)
        {
            if (!p.activeInHierarchy) continue;

            float d = Vector2.SqrMagnitude(p.transform.position - transform.position);
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
                ph.TakeDamage(1);
            }
        }
    }
}
