using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, int shooterID = 0)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(shooterID);
        }
    }

    void Die(int shooterID)
    {
        // Endless Survival
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddKill();
        }

        // Two Player Battle
        if (TwoPlayerGameManager.Instance != null && shooterID > 0)
        {
            TwoPlayerGameManager.Instance.AddKill(shooterID);
        }

        Destroy(gameObject);
    }
}

