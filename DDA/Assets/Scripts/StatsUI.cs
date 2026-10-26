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

    void Update()
    {
        if (StatisticManager.Instance == null || statsText == null)
            return;

        StatisticManager stats = StatisticManager.Instance;

        statsText.text =
            $"--- PLAYER STATS ---\n" +
            $"Kills: {stats.Kills}\n" +
            $"Shots Fired: {stats.ShotsFired}\n" +
            $"Shots Hit: {stats.ShotsHit}\n" +
            $"Accuracy: {stats.Accuracy:P1}\n" +
            $"Damage Taken: {stats.DamageTaken}\n" +
            $"Survival Time: {stats.SurvivalTime:F1}s\n\n" +

            $"--- DDA DEBUG ---\n" +
            $"DDA Enabled: {ddaEnabled}\n" +
            $"Difficulty Level: {difficultyLevel}\n" +
            $"Performance Score: {performanceScore:F2}\n" +
            $"Enemy Spawn Interval: {enemySpawnInterval:F1}s\n" +
            $"Enemy Speed: {enemySpeed:F1}";
    }
}
