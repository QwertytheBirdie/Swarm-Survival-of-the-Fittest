using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // NEW (safe even if already installed)

public class CoopGameManager : MonoBehaviour
{
    public static CoopGameManager Instance;

    [Header("Player Join")]
    public PlayerInputManager inputManager;
    public int requiredPlayers = 2;

    [Header("Spawner")]
    public EnemyWaveSpawner spawner; // KEEP old spawner name

    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Wave")]
    public int currentWave = 1;

    public TMP_Text waveText; // NEW — drag your WaveText here

    private readonly List<PlayerHealth> players = new List<PlayerHealth>();
    private bool gameStarted = false;
    private bool waveEnding = false;

    private void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateWaveUI(); // NEW
        Time.timeScale = 1f;
    }

    // ---------------- PLAYER REGISTER ----------------
    public void RegisterPlayer(PlayerHealth p)
    {
        if (!players.Contains(p))
            players.Add(p);

        // Start ONLY when both players are in
        if (!gameStarted && players.Count >= requiredPlayers)
        {
            gameStarted = true;

            // 🔒 LOCK JOINING — THIS PREVENTS RESPAWNS
            if (inputManager != null)
                inputManager.enabled = false;

            UpdateWaveUI();

            if (spawner != null)
                spawner.StartWave(currentWave);
        }
    }


    public void TryJoinPlayer()
    {
        if (gameStarted) return;
        if (inputManager == null) return;

        if (PlayerInput.all.Count < requiredPlayers)
        {
            inputManager.JoinPlayer();
            Debug.Log("[JOIN] Player joined");
        }
    }


    // ---------------- PLAYER DEATH ----------------
    public void OnPlayerDied(PlayerHealth p) { HandlePlayerDeath(); }
    public void OnPlayerDie(PlayerHealth p) { HandlePlayerDeath(); }
    public void PlayerDied(PlayerHealth p) { HandlePlayerDeath(); }

    private void HandlePlayerDeath()
    {
        bool anyAlive = false;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null && players[i].IsAlive)
            {
                anyAlive = true;
                break;
            }
        }

        if (!anyAlive)
            GameOver();
    }

    // ---------------- WAVE COMPLETE ----------------
    public void OnWaveCompleted() { HandleWaveComplete(); }
    public void OnWaveComplete() { HandleWaveComplete(); }
    public void WaveCompleted() { HandleWaveComplete(); }

    private void HandleWaveComplete()
    {
        if (waveEnding) return;
        waveEnding = true;

        currentWave++;

        UpdateWaveUI();

        StartCoroutine(WaveDelayRoutine());
    }


    private void ResetWaveLock()
    {
        waveEnding = false;
    }


    // ---------------- GAME OVER ----------------
    private void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // ---------------- ENEMY TARGETING ----------------
    public Transform GetAlivePlayerTarget()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null && players[i].IsAlive)
                return players[i].transform;
        }
        return null;
    }

    // ---------------- WAVE UI ----------------
    private void UpdateWaveUI() // NEW
    {
        if (waveText != null)
            waveText.text = "WAVE " + currentWave;
    }
    
    [Header("Wave Timing")]
    public float timeBetweenWaves = 3f;

    private System.Collections.IEnumerator WaveDelayRoutine()
    {
        // Optional: show "Next Wave In..." UI here
        yield return new WaitForSeconds(timeBetweenWaves);

        // Respawn dead players (COD Zombies style)
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null && players[i].IsDead)
                players[i].Respawn();
        }

        if (spawner != null)
            spawner.StartWave(currentWave);

        waveEnding = false;
    }

}
