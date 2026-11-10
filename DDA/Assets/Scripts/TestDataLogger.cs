
using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class TestDataLogger : MonoBehaviour
{
    private string resultsFilePath;
    private string ddaFilePath;
    private string eventsFilePath;
    private string runId;

    private readonly CultureInfo culture =
        CultureInfo.InvariantCulture;

    void Awake()
    {
        runId = Guid.NewGuid().ToString("N");

        string folder = Application.persistentDataPath;

        resultsFilePath = Path.Combine(
            folder, "test_results.csv"
        );

        ddaFilePath = Path.Combine(
            folder, "dda_changes.csv"
        );

        eventsFilePath = Path.Combine(
            folder, "dda_events.csv"
        );

        // Pelikierrosten lopputulokset
        CreateFileIfMissing(
            resultsFilePath,
            "RunId,Timestamp,DDAEnabled,FinalDifficulty," +
            "Kills,ShotsFired,ShotsHit,Accuracy," +
            "DamageTaken,SurvivalTime,EndReason"
        );

        // DDA:n arviointitiedot
        CreateFileIfMissing(
            ddaFilePath,
            "RunId,GameTime,OldDifficulty,NewDifficulty," +
            "PerformanceScore,Kills,Accuracy,DamageTaken," +
            "WindowKills,WindowShots,WindowAccuracy," +
            "WindowDamage,ChangeReason"
        );

        // F1-näppäimellä tehdyt DDA-kytkennät
        CreateFileIfMissing(
            eventsFilePath,
            "RunId,GameTime,Event,OldDifficulty,NewDifficulty"
        );
    }

    void CreateFileIfMissing(string path, string header)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, header + "\n");
        }
    }

    public void LogDifficultyEvaluation(
        int oldDifficulty,
        int newDifficulty,
        float performanceScore,
        int windowKills,
        int windowShots,
        float windowAccuracy,
        int windowDamage,
        string changeReason)
    {
        StatisticManager stats = StatisticManager.Instance;

        if (stats == null)
            return;

        string row = string.Join(",",
            runId,
            stats.SurvivalTime.ToString(culture),
            oldDifficulty,
            newDifficulty,
            performanceScore.ToString(culture),
            stats.Kills,
            stats.Accuracy.ToString(culture),
            stats.DamageTaken,
            windowKills,
            windowShots,
            windowAccuracy.ToString(culture),
            windowDamage,
            changeReason
        );

        File.AppendAllText(ddaFilePath, row + "\n");
    }

    public void LogDDAEvent(
        string eventName,
        int oldDifficulty,
        int newDifficulty)
    {
        StatisticManager stats = StatisticManager.Instance;

        if (stats == null)
            return;

        string row = string.Join(",",
            runId,
            stats.SurvivalTime.ToString(culture),
            eventName,
            oldDifficulty,
            newDifficulty
        );

        File.AppendAllText(eventsFilePath, row + "\n");
    }

    public void SaveResults()
    {
        StatisticManager stats = StatisticManager.Instance;
        DifficultyManager dda = DifficultyManager.Instance;

        if (stats == null || dda == null)
            return;

        string endReason =
            GameManager.Instance != null &&
            GameManager.Instance.TimeLimitReached
                ? "TimeLimit"
                : "PlayerDeath";

        string row = string.Join(",",
            runId,
            DateTime.UtcNow.ToString("o"),
            dda.DDAEnabled,
            dda.DifficultyLevel,
            stats.Kills,
            stats.ShotsFired,
            stats.ShotsHit,
            stats.Accuracy.ToString(culture),
            stats.DamageTaken,
            stats.SurvivalTime.ToString(culture),
            endReason
        );

        File.AppendAllText(resultsFilePath, row + "\n");

        Debug.Log(
            "Test results saved: " + resultsFilePath +
            " | End reason: " + endReason
        );
    }
}
