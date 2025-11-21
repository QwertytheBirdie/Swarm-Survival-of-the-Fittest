using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Needed for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score & Timer")]
    public int killCount = 0;
    public float survivalTime = 0f;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI timerText;

    [Header("Game Over UI")]
    public GameObject gameOverUI;

    private bool isGameOver = false;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Hide Game Over panel at the start
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // Initialize UI
        UpdateKillUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (isGameOver) return;

        // Update survival timer
        survivalTime += Time.deltaTime;
        UpdateTimerUI();
    }

    // Call this when an enemy dies
    public void AddKill()
    {
        killCount++;
        if (killCountText != null)
            killCountText.text = "Kills: " + killCount;
    }


    private void UpdateKillUI()
    {
        if (killCountText != null)
            killCountText.text = "Kills: " + killCount;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + survivalTime.ToString("F1") + "s";
    }

    // Call this when the player dies
    public void GameOver()
    {
        isGameOver = true;

        // Show Game Over UI
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // Stop the game
        Time.timeScale = 0f;
    }

    // Button callback to restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Button callback to return to Main Menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Make sure this matches your main menu scene name
    }
}
