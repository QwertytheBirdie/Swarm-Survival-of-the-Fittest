using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float basespeed;

    private Transform target;
    private Collider2D col;
    private PlayerHealth lastKilledPlayer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        basespeed = moveSpeed;
        // ❌ HP DOES NOT BELONG HERE — REMOVED
    }

    void Update()
    {
        if (CoopGameManager.Instance == null) return;

        target = CoopGameManager.Instance.GetAlivePlayerTarget();

        // If target changed (new alive player), re-enable collider
        if (target != null)
        {
            PlayerHealth ph = target.GetComponent<PlayerHealth>();
            if (ph != null && ph != lastKilledPlayer)
            {
                col.enabled = true;
            }
        }

        int wave = CoopGameManager.Instance.currentWave;

        // Gradually increase speed (THIS IS GOOD)
        moveSpeed = basespeed + (wave * 0.1f);

        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph == null || !ph.IsAlive) return;

        ph.KillPlayer();
        lastKilledPlayer = ph;

        // Prevent spam-kill
        col.enabled = false;
    }
}
