using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private int enemyHealth = 3;
    [SerializeField] private int maxEnemies = 20;
    [SerializeField] private int enemiesPerSpawn = 1;

    private int activeEnemies;

    public int ActiveEnemies => activeEnemies;
    public int MaxEnemies => maxEnemies;

    public int EnemiesPerSpawn => enemiesPerSpawn;

    public float EnemySpeed => enemySpeed;
    public int EnemyHealth => enemyHealth;

    private float nextSpawnTime;

    public float SpawnInterval => spawnInterval;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (activeEnemies >= maxEnemies)
                    break;

                SpawnEnemy();
            }

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0 || activeEnemies >= maxEnemies)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform selectedSpawnPoint = spawnPoints[randomIndex];

        GameObject enemyObject = Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        activeEnemies++;

        if (enemyObject.TryGetComponent<EnemyMovement>(out EnemyMovement movement))
        {
            movement.SetMoveSpeed(enemySpeed);
        }

        if (enemyObject.TryGetComponent<EnemyHealth>(out EnemyHealth health))
        {
            health.SetMaxHealth(enemyHealth);
            health.SetSpawner(this);
        }
    }

    public void EnemyRemoved()
    {
        activeEnemies = Mathf.Max(0, activeEnemies - 1);
    }

    public void SetEnemyStats(float speed, int health)
    {
        enemySpeed = Mathf.Max(0f, speed);
        enemyHealth = Mathf.Max(1, health);
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = Mathf.Max(0.2f, interval);
    }

    public void SetSpawnSettings(float interval, int count, int maximum)
    {
        spawnInterval = Mathf.Max(0.2f, interval);
        enemiesPerSpawn = Mathf.Max(1, count);
        maxEnemies = Mathf.Max(1, maximum);
    }
}
