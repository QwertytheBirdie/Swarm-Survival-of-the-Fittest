using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootCooldown = 1f;

    public Transform firePoint; // Where the bullets will spawn


    private float shootTimer = 0f;
    private Camera cam;
    private Rigidbody2D rb;

    public int killCount = 0;
    public float survivalTime = 0f;

    public TMPro.TextMeshProUGUI killCountText;
    public TMPro.TextMeshProUGUI timerText;

    private bool isGameOver = false;


    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Aim();
        Shoot();

        if (bulletPrefab == null)
            Debug.LogError("bulletPrefab is null!");

        if (firePoint == null)
            Debug.LogError("firePoint is null!");


        if (isGameOver) return;
        survivalTime += Time.deltaTime;
        timerText.text = "Time: " + survivalTime.ToString("F1");

    }
    void Aim()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle; // rotate player to face cursor
    }

    void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shootTimer <= 0f)
        {
            shootTimer = shootCooldown;
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDir = (mousePos - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = shootDir * bulletSpeed;
        }
    }
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDir * moveSpeed;

        // Clamp the player to stay on screen
        Vector3 pos = transform.position;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        pos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = pos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }

    public void AddKill()
    {
        killCount++;
        killCountText.text = "Kills: " + killCount;
    }


}
