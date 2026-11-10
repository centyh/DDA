using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StatisticManager.Instance.RegisterDamage(damage);
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.EndGame();
            gameObject.SetActive(false);
        }
    }
}
