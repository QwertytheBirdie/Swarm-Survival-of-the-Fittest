using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlayerShooter : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Fire Settings")]
    public float fireRate = 0.5f;

    [Header("Audio")]
    public AudioClip shootSound;
    public float shootVolume = 1f;

    private float nextFireTime;
    private AudioSource audioSource;
    private PlayerInput playerInput;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    // CALLED BY PLAYER INPUT EVENT
    public void TryShoot()
    {
        if (Time.time < nextFireTime)
            return;

        if (bulletPrefab == null || firePoint == null)
            return;

        nextFireTime = Time.time + fireRate;

        GameObject bulletObj = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // 🔥 CRITICAL LINE — THIS FIXES EVERYTHING
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            // PlayerInput index: 0 → P1, 1 → P2
            bullet.shooterID = playerInput.playerIndex + 1;
        }

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound, shootVolume);
    }
}
