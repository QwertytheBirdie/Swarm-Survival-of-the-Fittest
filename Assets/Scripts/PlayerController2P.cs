using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
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

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move
        Vector2 velocity = moveInput * moveSpeed;
        rb.linearVelocity = velocity;

        // Aim / rotate
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            transform.rotation = rot;
            if (firePoint != null)
                firePoint.rotation = rot;
        }
    }

    // --------- Input System callbacks (wired in PlayerInput) ----------

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
