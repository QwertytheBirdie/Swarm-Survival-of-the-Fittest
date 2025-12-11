using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shooter Info")]
    public int playerIndex = 1;      // 1 for Player1, 2 for Player2

    [Header("References")]
    public GameObject bulletPrefab;  // Blue or Red bullet
    public Transform firePoint;

    [Header("Fire Settings")]
    public float fireRate = 0.2f;    // seconds between shots

    private float nextFireTime = 0f;

    [Header("Audio")]
    public AudioClip shootSound;     // assign in inspector
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        if (bulletPrefab == null || firePoint == null) return;

        nextFireTime = Time.time + fireRate;

        // Spawn bullet
        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.shooterID = playerIndex;
        }

        // Play shooting sound (does not break battle mode)
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
