using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Test Settings")]
    [SerializeField] private float timeLimit = 180f;

    private bool gameOver;
    private bool timeLimitReached;

    public bool TimeLimitReached => timeLimitReached;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!gameOver &&
            StatisticManager.Instance != null &&
            StatisticManager.Instance.SurvivalTime >= timeLimit)
        {
            timeLimitReached = true;
            EndGame();
        }

        if (gameOver &&
            Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void EndGame()
    {
        if (gameOver)
            return;

        gameOver = true;

        if (DifficultyManager.Instance != null)
        {
            DifficultyManager.Instance.StopDDA();
        }

        if (StatisticManager.Instance != null)
        {
            StatisticManager.Instance.StopTracking();
        }

        TestDataLogger logger =
            FindFirstObjectByType<TestDataLogger>();

        if (logger != null)
        {
            logger.SaveResults();
        }

        if (timeLimitReached)
        {
            Debug.Log("Time limit reached! Press R to restart.");

        }
        else
        {
            Debug.Log("Game Over! Press R to restart.");
            
        }

        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
