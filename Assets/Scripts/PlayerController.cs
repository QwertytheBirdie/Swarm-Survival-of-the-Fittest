using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    [Header("References")]
    public Camera cam;
    private Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;

        if (firePoint == null)
            Debug.LogError("FirePoint not assigned!");
        if (bulletPrefab == null)
            Debug.LogError("BulletPrefab not assigned!");
    }

    void Update()
    {
        // ---- Movement input ----
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ---- Mouse position ----
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        // ---- Shooting (click only) ----
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        // ---- Move player ----
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // ---- Rotate player toward cursor ----
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // No offset needed because sprite faces RIGHT
        rb.rotation = angle;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
