using UnityEngine;
using UnityEngine.UI;

public class AsteroidsPassedController : MonoBehaviour {
    private GameplayController _gc;
    private StatsController _statsController;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
        _statsController = FindObjectOfType<StatsController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (_gc.gameOver) return;
        if (!other.gameObject.CompareTag("Deadly")) return;
        
        ++_statsController.AsteroidsPassed;
        _statsController.CurrentScore += 5;
    }
}