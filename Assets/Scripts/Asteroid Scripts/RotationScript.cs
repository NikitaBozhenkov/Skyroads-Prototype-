using UnityEngine;

public class RotationScript : MonoBehaviour {
    private GameplayController _gc;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
    }

    private void OnCollisionEnter(Collision other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        other.gameObject.SetActive(false);
        _gc.GameOver();
    }

    private void Update() {
        transform.Rotate(new Vector3(0, Time.deltaTime * 100f, 0));
    }
}