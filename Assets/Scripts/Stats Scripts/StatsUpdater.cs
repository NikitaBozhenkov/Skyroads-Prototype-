using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour {
    private GameplayController _gc;
    private StatsController _statsController;

    [SerializeField] private GameObject[] texts;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text asteroidsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text highScoreText;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
        _statsController = FindObjectOfType<StatsController>();
    }

    private void Update() {
        if (_gc.gameOver) {
            foreach (var text in texts) {
                text.SetActive(false);
            }

            // It might be a panel, but let it be so
            scoreText.gameObject.SetActive(false);
            asteroidsText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
            highScoreText.gameObject.SetActive(false);
            return;
        }

        scoreText.text = _statsController.CurrentScore.ToString("0");
        asteroidsText.text = _statsController.AsteroidsPassed.ToString("0");
        timeText.text = _statsController.GameTime.ToString("0") + "s";
        highScoreText.text = _statsController.HighScore.ToString("0");
    }
}