using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool gameOver;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (gameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void EndGame()
    {
        if (gameOver) return;

        gameOver = true;
        StatisticManager.Instance.StopTracking();
        GetComponent<TestDataLogger>()?.SaveResults();
        Debug.Log("Game Over! Press R to restart.");
    }

    void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
