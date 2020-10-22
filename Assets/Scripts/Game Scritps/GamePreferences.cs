using UnityEngine;

public static class GamePreferences {
    public static string Highscore = "Highscore";

    public static float GetHighscore() {
        return PlayerPrefs.GetFloat(Highscore);
    }

    public static void SetHighscore(float score) {
        PlayerPrefs.SetFloat(Highscore, score);
    }
}