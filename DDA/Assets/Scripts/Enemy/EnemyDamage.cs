using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageInterval = 1f;

    private float nextDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            if (Time.time >= nextDamageTime)
            {
                player.TakeDamage(damage);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
