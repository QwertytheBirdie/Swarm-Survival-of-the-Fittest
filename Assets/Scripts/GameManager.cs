using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI timerText;

    [Header("Gameplay")]
    public bool isGameOver = false;
    private float survivalTime = 0f;
    private int kills = 0;
    
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        // Hide Game Over panel at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Reset stats
        survivalTime = 0f;
        kills = 0;

        UpdateUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
            UpdateUI();
        }
    }

    public void AddKill()
    {
        kills++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (killCountText != null)
            killCountText.text = "Kills: " + kills;

        if (timerText != null)
            timerText.text = "Time: " + survivalTime.ToString("F1");
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Make sure your main menu is Scene 0
    }
}
