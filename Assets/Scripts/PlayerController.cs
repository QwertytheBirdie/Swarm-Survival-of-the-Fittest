using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform firePoint;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private SpriteRenderer sr;

    private Vector2 moveInput;
    private Vector2 aimInput;

    public Sprite player1Sprite;
    public Sprite player2Sprite;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ApplyAppearance();
        // playerIndex is valid here
        if (playerInput.playerIndex == 0)
            sr.color = Color.cyan;
        else
            sr.color = Color.red;
    }


    void ApplyAppearance()
    {
        int index = playerInput.playerIndex;

        if (index == 0)
        {
            sr.color = Color.cyan;  // Player 1
        }
        else
        {
            sr.color = Color.red;   //Player 2
        }

        SpriteRenderer fpSR = firePoint.GetComponent<SpriteRenderer>();
        if (fpSR != null)
        {
            fpSR.color = (playerInput.playerIndex == 0)
                ? Color.cyan
                : Color.red;
        }
        
        if (playerInput.playerIndex == 0)
        {
            sr.sprite = player1Sprite;
            sr.color = Color.white;
        }
        else
        {
            sr.sprite = player2Sprite;
            sr.color = Color.white;
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        if (aimInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            transform.rotation = rot;

            if (firePoint != null)
                firePoint.rotation = rot;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        aimInput = ctx.ReadValue<Vector2>();
    }
}

