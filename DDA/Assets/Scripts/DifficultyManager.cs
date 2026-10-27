using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("DDA Settings")]
    [SerializeField] private bool ddaEnabled = true;
    [SerializeField] private float evaluationInterval = 15f;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Difficulty")]
    [SerializeField, Range(1, 5)]
    private int difficultyLevel = 1;

    public bool DDAEnabled => ddaEnabled;
    public int DifficultyLevel => difficultyLevel;
    public float PerformanceScore { get; private set; }

    private float nextEvaluationTime;
    private bool isRunning = true;

    private int previousKills;
    private int previousShots;
    private int previousHits;
    private int previousDamage;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ApplyDifficulty();
        nextEvaluationTime = Time.time + evaluationInterval;
    }

    void Update()
    {
        if (!ddaEnabled || !isRunning ||
            StatisticManager.Instance == null)
            return;

        if (Time.time >= nextEvaluationTime)
        {
            EvaluatePerformance();
            nextEvaluationTime = Time.time + evaluationInterval;
        }
    }

    void EvaluatePerformance()
    {
        StatisticManager stats = StatisticManager.Instance;

        int kills = stats.Kills - previousKills;
        int shots = stats.ShotsFired - previousShots;
        int hits = stats.ShotsHit - previousHits;
        int damage = stats.DamageTaken - previousDamage;

        float accuracy = shots > 0 ? (float)hits / shots : 0f;

        float killScore = Mathf.Clamp01(kills / 5f);
        float accuracyScore = Mathf.Clamp01(accuracy);
        float damageScore = 1f - Mathf.Clamp01(damage / 5f);

        PerformanceScore =
            killScore * 0.4f +
            accuracyScore * 0.3f +
            damageScore * 0.3f;

        if (PerformanceScore >= 0.7f)
        {
            difficultyLevel++;
        }
        else if (PerformanceScore <= 0.3f)
        {
            difficultyLevel--;
        }

        difficultyLevel = Mathf.Clamp(difficultyLevel, 1, 5);

        ApplyDifficulty();

        previousKills = stats.Kills;
        previousShots = stats.ShotsFired;
        previousHits = stats.ShotsHit;
        previousDamage = stats.DamageTaken;

        Debug.Log(
            $"DDA | Score: {PerformanceScore:F2} | " +
            $"Difficulty: {difficultyLevel}"
        );
    }

    void ApplyDifficulty()
    {
        if (enemySpawner == null)
            return;

        float spawnInterval = 4f - (difficultyLevel - 1) * 0.5f;
        float enemySpeed = 2f + (difficultyLevel - 1) * 0.4f;
        int enemyHealth = 3 + (difficultyLevel - 1);

        enemySpawner.SetSpawnInterval(spawnInterval);
        enemySpawner.SetEnemyStats(enemySpeed, enemyHealth);
    }

    public void StopDDA()
    {
        isRunning = false;
    }
}
