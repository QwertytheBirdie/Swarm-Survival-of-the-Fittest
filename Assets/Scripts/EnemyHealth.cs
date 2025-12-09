using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, int shooterID)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(shooterID);
        }
    }

    void Die(int shooterID)
    {
        // Two Player Battle mode: Add kill to GameManager
        if (TwoPlayerGameManager.Instance != null)
        {
            TwoPlayerGameManager.Instance.AddKill(shooterID);
        }

        Destroy(gameObject);
    }
}
