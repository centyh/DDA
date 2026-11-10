using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    private EnemySpawner spawner;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StatisticManager.Instance.RegisterKill();

            if (spawner != null)
                spawner.EnemyRemoved();

            Destroy(gameObject);
        }
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = Mathf.Max(1, health);
        currentHealth = maxHealth;
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }
}
