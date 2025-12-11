using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    private Vector2 moveInput;

    [Header("Aiming")]
    public Transform firePoint;
    private Vector2 aimInput;

    [Header("Shooting")]
    public PlayerShooter shooter;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movement using left stick
        rb.linearVelocity = moveInput * moveSpeed;

        // Aiming using right stick
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (firePoint != null)
                firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // ---------- INPUT SYSTEM CALLBACKS ----------

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
            shooter.TryShoot();
    }
}
