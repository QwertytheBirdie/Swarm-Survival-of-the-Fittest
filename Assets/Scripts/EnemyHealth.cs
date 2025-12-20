using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 1;

    private int hp;
    private bool isDead = false;
    private EnemyWaveSpawner spawner;

    private void Awake()
    {
        int wave = 1;

        if (CoopGameManager.Instance != null)
            wave = CoopGameManager.Instance.currentWave;

        // ✅ HP scaling per wave (safe)
        hp = maxHP + Mathf.FloorToInt(wave * 0.25f);
    }

    public void SetSpawner(EnemyWaveSpawner s)
    {
        spawner = s;
    }

    public void TakeDamage(int damage, int shooterID)
    {
        if (isDead) return;

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (spawner != null)
        {
            spawner.EnemyKilled();
        }
        else
        {
            Debug.LogError("[ENEMY] Spawner not assigned!");
        }

        Destroy(gameObject);
    }
}
