using UnityEngine;
using TMPro;

public class TwoPlayerGameManager : MonoBehaviour
{
    public static TwoPlayerGameManager Instance;

    [Header("Match Settings")]
    public float matchDuration = 180f; // 3 minutes
    private float timer;

    [Header("Player Status")]
    public bool player1Alive = true;
    public bool player2Alive = true;

    [Header("Score Tracking")]
    public int player1Kills = 0;
    public int player2Kills = 0;

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text player1KillsText;
    public TMP_Text player2KillsText;
    public GameObject winnerPanel;
    public TMP_Text winnerText;

    private bool matchEnded = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = matchDuration;

        // Hide winner UI at start
        if (winnerPanel != null)
            winnerPanel.SetActive(false);

        UpdateKillUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (matchEnded) return;

        // Countdown timer
        timer -= Time.deltaTime;

        UpdateTimerUI();

        if (timer <= 0f)
        {
            EndBattle();
        }

        // If both players dead early → end immediately
        if (!player1Alive && !player2Alive)
        {
            EndBattle();
        }
    }

    // -------------------------------------
    // ADDING KILLS
    // -------------------------------------
    public void AddKill(int shooterID)
    {
        if (shooterID == 1)
            player1Kills++;
        else if (shooterID == 2)
            player2Kills++;

        UpdateKillUI();
    }

    // -------------------------------------
    // PLAYER DEATH HANDLING
    // -------------------------------------
    public void OnPlayerDied(int index)
    {
        if (index == 1) player1Alive = false;
        if (index == 2) player2Alive = false;

        // If both players die → end the match
        if (!player1Alive && !player2Alive)
        {
            EndBattle();
        }
    }

    // -------------------------------------
    // ENDING THE MATCH
    // -------------------------------------
    public void EndBattle()
    {
        if (matchEnded) return;

        matchEnded = true;

        // Stop all enemy spawners if you have them
        EnemySpawner_Battle[] spawners = FindObjectsOfType<EnemySpawner_Battle>();
        foreach (var s in spawners)
        {
            s.StopSpawning();
        }

        // Determine winner
        int winner = 0;

        if (player1Kills > player2Kills)
            winner = 1;
        else if (player2Kills > player1Kills)
            winner = 2;
        else
            winner = 0; // tie

        // Activate win screen
        if (winnerPanel != null)
            winnerPanel.SetActive(true);

        if (winnerText != null)
        {
            if (winner == 1)
                winnerText.text = "Player 1 Wins!";
            else if (winner == 2)
                winnerText.text = "Player 2 Wins!";
            else
                winnerText.text = "Draw!";
        }

        Debug.Log("Battle Ended. Winner: " + winner);
    }

    // -------------------------------------
    // UI UPDATES
    // -------------------------------------
    void UpdateKillUI()
    {
        if (player1KillsText != null)
            player1KillsText.text = "P1 Kills: " + player1Kills;

        if (player2KillsText != null)
            player2KillsText.text = "P2 Kills: " + player2Kills;
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    // -------------------------------------
    // CALLED BY UI BUTTON (OPTIONAL)
    // -------------------------------------
    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
