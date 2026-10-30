
using System;
using System.IO;
using UnityEngine;

public class TestDataLogger : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        filePath = Path.Combine(
            Application.persistentDataPath,
            "test_results.csv"
        );

        if (!File.Exists(filePath))
        {
            File.WriteAllText(
                filePath,
                "Timestamp,DDAEnabled,FinalDifficulty,Kills,ShotsFired,ShotsHit,Accuracy,DamageTaken,SurvivalTime\n"
            );
        }
    }

    public void SaveResults()
    {
        StatisticManager stats = StatisticManager.Instance;
        DifficultyManager dda = DifficultyManager.Instance;

        if (stats == null || dda == null)
            return;

        string row = string.Join(",",
            DateTime.UtcNow.ToString("o"),
            dda.DDAEnabled,
            dda.DifficultyLevel,
            stats.Kills,
            stats.ShotsFired,
            stats.ShotsHit,
            stats.Accuracy.ToString(
                System.Globalization.CultureInfo.InvariantCulture),
            stats.DamageTaken,
            stats.SurvivalTime.ToString(
                System.Globalization.CultureInfo.InvariantCulture)
        );

        File.AppendAllText(filePath, row + "\n");

        Debug.Log("Test results saved: " + filePath);
    }
}
