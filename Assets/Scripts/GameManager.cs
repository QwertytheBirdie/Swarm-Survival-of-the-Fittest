using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // GLOBAL ACCESS

    [Header("Gameplay State")]
    public bool isGameOver = false;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI timerText;

    [Header("Gameplay Stats")]
    public int killCount = 0;
    public float survivalTime = 0f;

    private void Awake()
    {
        // Make this a globally accessible singleton
        Instance = this;
    }

    private void Start()
    {
        // Be sure Game Over screen is hidden at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateKillUI();
        UpdateTimerUI();

        // Ensure game is running at normal speed
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    // -----------------------------
    //       KILL COUNT SYSTEM
    // -----------------------------
    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
    }

    private void UpdateKillUI()
    {
        if (killCountText != null)
            killCountText.text = "Kills: " + killCount;
    }

    // -----------------------------
    //       TIMER UI SYSTEM
    // -----------------------------
    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + survivalTime.ToString("F1");
    }

    // -----------------------------
    //        GAME OVER LOGIC
    // -----------------------------
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0f; // freeze gameplay

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER triggered by GameManager.");
    }

    // 🔹 NEW: called by PlayerHealth when HP reaches 0
    public void PlayerDied()
    {
        // For now, just reuse your existing GameOver flow
        GameOver();
    }

    // -----------------------------
    //      BUTTON FUNCTIONS
    // -----------------------------
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Main Menu should be scene 0
    }

    // -----------------------------
    //     DIFFICULTY MULTIPLIER
    // -----------------------------
    public float GetDifficultyMultiplier()
    {
        return 1f + (survivalTime / 30f);
    }

}
