using UnityEngine;

public class StatsController : MonoBehaviour {
    private GameplayController _gc;

    public float AsteroidsPassed { get; set; }
    public float CurrentScore { get; set; }
    public float HighScore { get; private set; }
    public float GameTime { get; private set; }
    public bool IsTheHighScoreBeaten { get; private set; }

    [SerializeField] private int pointPerUnit = 3;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
    }

    private void Start() {
        CurrentScore = 0;
        GameTime = 0;
        IsTheHighScoreBeaten = false;
        HighScore = GamePreferences.GetHighscore();
    }

    private void Update() {
        if (_gc.gameOver) {
            GamePreferences.SetHighscore(HighScore);
            return;
        }

        if (_gc.isGameBoosted) {
            CurrentScore += Time.deltaTime * pointPerUnit * 2;
        } else {
            CurrentScore += Time.deltaTime * pointPerUnit;
        }

        if (CurrentScore > HighScore) {
            IsTheHighScoreBeaten = true;
            HighScore = CurrentScore;
        }

        GameTime = Time.timeSinceLevelLoad;
    }
}