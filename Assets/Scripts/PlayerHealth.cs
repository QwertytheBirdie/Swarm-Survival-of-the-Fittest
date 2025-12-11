using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Tell GameManager the player died
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }

        // Disable the player object so it disappears / stops moving
        gameObject.SetActive(false);
    }
}
