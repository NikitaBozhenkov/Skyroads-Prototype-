using UnityEngine;
using UnityEngine.UI;

public class GameOverStatsViewer : MonoBehaviour {
    private StatsController _statsController;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text asteroidsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text congratulations;

    private void Awake() {
        _statsController = FindObjectOfType<StatsController>();
    }

    public void UpdateGameOverStats() {
        scoreText.text = _statsController.CurrentScore.ToString("0");
        asteroidsText.text = _statsController.AsteroidsPassed.ToString("0");
        timeText.text = _statsController.GameTime.ToString("0") + "s";
        congratulations.gameObject.SetActive(_statsController.IsTheHighScoreBeaten);
    }
    
    
}