using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform targetPlayer;

    void Update()
    {
        FindClosestPlayer();
        MoveTowardPlayer();
    }

    // ---------------------------------------------------------
    // Move toward the closest player
    // ---------------------------------------------------------
    void MoveTowardPlayer()
    {
        if (targetPlayer == null) return;

        // Move
        Vector2 newPos = Vector2.MoveTowards(
            transform.position,
            targetPlayer.position,
            moveSpeed * Time.deltaTime
        );
        transform.position = newPos;

        // Rotate to face the player
        Vector2 dir = targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ---------------------------------------------------------
    // Find the closest player object tagged "Player"
    // ---------------------------------------------------------
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

    // ---------------------------------------------------------
    // Kill the player on physical collision
    // Requires BOTH colliders to NOT be triggers
    // ---------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            PlayerHealth ph = col.collider.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(9999); // Instant death
            }
        }
    }
}
