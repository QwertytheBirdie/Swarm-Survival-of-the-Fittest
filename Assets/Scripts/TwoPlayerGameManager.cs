using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TwoPlayerGameManager : MonoBehaviour
{
    public static TwoPlayerGameManager Instance;

    [Header("Score Tracking")]
    public int p1Kills = 0;
    public int p2Kills = 0;

    [Header("Timer")]
    public float matchDuration = 180f;
    private float timeRemaining;
    private bool matchEnded = false;

    [Header("UI")]
    public TextMeshProUGUI p1KillsText;
    public TextMeshProUGUI p2KillsText;
    public TextMeshProUGUI timerText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timeRemaining = matchDuration;

        if (resultPanel != null)
            resultPanel.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (matchEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndMatch();
        }

        UpdateUI();
    }

    public void AddKill(int shooterID)
    {
        if (shooterID == 1) p1Kills++;
        if (shooterID == 2) p2Kills++;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (p1KillsText != null)
            p1KillsText.text = p1Kills.ToString();

        if (p2KillsText != null)
            p2KillsText.text = p2Kills.ToString();

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    void EndMatch()
    {
        matchEnded = true;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        string winner;

        if (p1Kills > p2Kills) winner = "Player 1 Wins!";
        else if (p2Kills > p1Kills) winner = "Player 2 Wins!";
        else winner = "It's a Tie!";

        if (resultText != null)
            resultText.text = winner;
    }

    public void RestartMatch()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
