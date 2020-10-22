using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {
    public bool gameOver;
    public bool isGameBoosted;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;


    private void Awake() {
        InitializeStats();
    }

    private void InitializeStats() {
        // If prefs aren't initialized
        if (PlayerPrefs.HasKey("Skyroads Initialized")) return;
        PlayerPrefs.SetInt("Skyroads Initialized", 1);
        GamePreferences.SetHighscore(0f);
    }

    private void Start() {
        Time.timeScale = 0f;
        gameOver = false;
        isGameBoosted = false;
        gameOverPanel.SetActive(false);
    }

    private void Update() {
        if (!startPanel.activeSelf) return;
        if (!Input.anyKeyDown) return;
        
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver() {
        gameOver = true;
        Invoke(nameof(ShowGameOverPanel), 2f);
    }

    private void ShowGameOverPanel() {
        var g = FindObjectOfType<GameOverStatsViewer>();
        g.UpdateGameOverStats();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame() {
       Application.Quit();
    }

    public void RestartGame() {
        startPanel.SetActive(true);
        SceneManager.LoadScene("Gameplay");
    }
}