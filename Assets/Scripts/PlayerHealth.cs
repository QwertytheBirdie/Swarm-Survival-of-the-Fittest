using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;
    public bool IsDead => !IsAlive;

    private PlayerInput playerInput;
    private Collider2D col;
    private SpriteRenderer sr;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Register player once
        if (CoopGameManager.Instance != null)
            CoopGameManager.Instance.RegisterPlayer(this);
    }

    // ---------------- PLAYER DEATH ----------------
    public void KillPlayer()
    {
        if (!IsAlive) return;

        IsAlive = false;

        // Disable ALL player interaction
        if (playerInput != null)
            playerInput.enabled = false;

        if (col != null)
            col.enabled = false;

        if (sr != null)
            sr.enabled = false;

        // Notify manager
        if (CoopGameManager.Instance != null)
            CoopGameManager.Instance.OnPlayerDied(this);
    }

    // ---------------- RESPAWN (NEXT WAVE ONLY) ----------------
    public void Respawn()
    {
        IsAlive = true;

        // Re-enable player fully
        if (playerInput != null)
            playerInput.enabled = true;

        if (col != null)
            col.enabled = true;

        if (sr != null)
            sr.enabled = true;

        // Reset position if you want (optional)
        // transform.position = CoopGameManager.Instance.GetSpawnPoint();

        Debug.Log("[PLAYER] Respawned at new wave");
    }
}
