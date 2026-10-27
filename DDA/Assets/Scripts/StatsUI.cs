using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsText;

    [Header("DDA Debug Values")]
    [SerializeField] private bool ddaEnabled = false;
    [SerializeField] private int difficultyLevel = 1;
    [SerializeField, Range(0f, 1f)]
    private float performanceScore = 0f;
    [SerializeField] private float enemySpawnInterval = 3f;
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private EnemySpawner enemySpawner;

    void Update()
    {
        StatisticManager stats = StatisticManager.Instance;
        DifficultyManager dda = DifficultyManager.Instance;

        if (stats == null || dda == null ||
            enemySpawner == null || statsText == null)
            return;

        statsText.text =
            "--- PLAYER STATS ---\n" +
            $"Kills: {stats.Kills}\n" +
            $"Shots Fired: {stats.ShotsFired}\n" +
            $"Shots Hit: {stats.ShotsHit}\n" +
            $"Accuracy: {stats.Accuracy:P1}\n" +
            $"Damage Taken: {stats.DamageTaken}\n" +
            $"Survival Time: {stats.SurvivalTime:F1}s\n\n" +

            "--- DDA DEBUG ---\n" +
            $"DDA Enabled: {dda.DDAEnabled}\n" +
            $"Difficulty Level: {dda.DifficultyLevel}\n" +
            $"Performance Score: {dda.PerformanceScore:F2}\n" +
            $"Enemy Spawn Interval: {enemySpawner.SpawnInterval:F1}s\n" +
            $"Enemy Speed: {enemySpawner.EnemySpeed:F1}\n" +
            $"Enemy Health: {enemySpawner.EnemyHealth}";
    }
}
