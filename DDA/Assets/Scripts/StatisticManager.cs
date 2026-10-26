using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    public static StatisticManager Instance { get; private set; }

    public int Kills { get; private set; }
    public int ShotsFired { get; private set; }
    public int ShotsHit { get; private set; }
    public int DamageTaken { get; private set; }
    public float SurvivalTime { get; private set; }

    public float Accuracy =>
        ShotsFired > 0 ? (float)ShotsHit / ShotsFired : 0f;

    private bool isRunning = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isRunning)
        {
            SurvivalTime += Time.deltaTime;
        }
    }

    public void RegisterKill() => Kills++;
    public void RegisterShot() => ShotsFired++;
    public void RegisterHit() => ShotsHit++;
    public void RegisterDamage(int amount) => DamageTaken += amount;

    public void StopTracking()
    {
        isRunning = false;

        Debug.Log(
            $"Round stats | Kills: {Kills}, " +
            $"Shots: {ShotsFired}, " +
            $"Hits: {ShotsHit}, " +
            $"Accuracy: {Accuracy:P1}, " +
            $"Damage taken: {DamageTaken}, " +
            $"Survival: {SurvivalTime:F1}s"
        );
    }
}
