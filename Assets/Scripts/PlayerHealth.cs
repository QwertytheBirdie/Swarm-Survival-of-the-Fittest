using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " died.");
        // For Endless Survival, trigger game over here
        // For 2P Battle you might just disable the player
        gameObject.SetActive(false);
    }
}
