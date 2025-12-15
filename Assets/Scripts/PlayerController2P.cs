using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController2P : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Aiming")]
    public Transform firePoint;
    private Vector2 aimInput;

    [Header("Shooting")]
    public PlayerShooter shooter;

    [Header("Visuals")]
    [Tooltip("Optional. If left empty, the script will auto-find the SpriteRenderer.")]
    public SpriteRenderer spriteRenderer;
    public Color player1Color = Color.blue;
    public Color player2Color = Color.red;

    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private int resolvedPlayerIndex; // 1 or 2

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        //--------------------------------------------------
        // RESOLVE PLAYER INDEX (Unity gives 0,1 → we want 1,2)
        //--------------------------------------------------
        resolvedPlayerIndex = playerInput.playerIndex + 1;

        //--------------------------------------------------
        // ASSIGN SHOOTER OWNERSHIP
        //--------------------------------------------------
        if (shooter != null)
        {
            shooter.playerIndex = resolvedPlayerIndex;
        }
        else
        {
            Debug.LogWarning($"[{name}] PlayerShooter is NOT assigned.");
        }

        //--------------------------------------------------
        // RESOLVE SPRITE RENDERER SAFELY
        //--------------------------------------------------
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color =
                resolvedPlayerIndex == 1 ? player1Color : player2Color;
        }
        else
        {
            Debug.LogWarning($"[{name}] No SpriteRenderer found!");
        }

        Debug.Log($"Spawned Player {resolvedPlayerIndex} ({(resolvedPlayerIndex == 1 ? "Blue" : "Red")})");
    }

    private void FixedUpdate()
    {
        //----------------------
        // MOVEMENT
        //----------------------
        rb.linearVelocity = moveInput * moveSpeed;

        //----------------------
        // AIMING / ROTATION
        //----------------------
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            transform.rotation = rot;

            if (firePoint != null)
                firePoint.rotation = rot;
        }
    }

    // ==================================================
    // INPUT SYSTEM CALLBACKS (Invoke Unity Events)
    // ==================================================

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        aimInput = ctx.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && shooter != null)
        {
            shooter.TryShoot();
        }
    }
}
