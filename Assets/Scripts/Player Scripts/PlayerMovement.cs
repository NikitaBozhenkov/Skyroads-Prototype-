using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    private GameplayController _gc;
    
    private const float RotationDelay = 0.01f;
    private const float RotateValue = 0.1f;
    private const float MaxAngle = 40f;
    private const float SpeedMultiplier = 1.5f;

    [SerializeField] private float speed;

    private IEnumerator RotateLeft(float angle) {
        while (transform.rotation.z > angle / 180) {
            yield return new WaitForSeconds(RotationDelay);
            transform.Rotate(0,0,-RotateValue);
        }
    }

    private IEnumerator RotateRight(float angle) {
        while (transform.rotation.z < angle / 180) {
            yield return new WaitForSeconds(RotationDelay);
            transform.Rotate(0,0,RotateValue);
        }
    }

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
    }

    private void Update() {
        float localSpeed;
        
        // Game boost
        if (_gc.isGameBoosted) localSpeed = speed * SpeedMultiplier;
        else localSpeed = speed / SpeedMultiplier;
        
        // Player movement
        var horizontal = Input.GetAxis("Horizontal");
        var direction = new Vector3(horizontal, 0f, 0f);
        controller.Move(direction * (localSpeed * Time.deltaTime));
        
        // Player rotation
        if (horizontal < 0) {
            StartCoroutine(RotateLeft(MaxAngle * horizontal));
        } else if (horizontal > 0) {
            StartCoroutine(RotateRight(MaxAngle * horizontal));
        } else {
            StartCoroutine(transform.rotation.z > 0.1f ? RotateLeft(0.1f) : RotateRight(0.1f));
        }

        // Game boost check
        if (Input.GetButtonDown("Boost") || Input.GetButton("Boost")) {
            _gc.isGameBoosted = true;
        } else if (Input.GetButtonUp("Boost") || !Input.GetButtonDown("Boost") || !Input.GetButton("Boost")) {
            _gc.isGameBoosted = false;
        }
    }
}