using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        if (collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            hasHit = true;

            if (StatisticManager.Instance != null)
            {
                StatisticManager.Instance.RegisterHit();
            }

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
