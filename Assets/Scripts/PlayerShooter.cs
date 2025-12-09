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

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        if (bulletPrefab == null || firePoint == null) return;

        nextFireTime = Time.time + fireRate;

        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.shooterID = playerIndex;
        }
    }
}
