using System;
using UnityEngine;

public class ObjectsForcer : MonoBehaviour {
    public Rigidbody rb;
    private GameplayController _gc;
    private float _lastForceValue;
    private bool _isSpeedBoosted;
    private bool _stopped;
    
    private const float ForwardForce = 4000f;
    private const float ForceMultiplier = 2f;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
    }

    private void Start() {
        rb.AddForce(0,0,-ForwardForce * Time.fixedDeltaTime);
        _lastForceValue = -ForwardForce * Time.fixedDeltaTime;
        _isSpeedBoosted = false;
    }


    private void Update() {
        // GameOver stop
        if (_gc.gameOver && !_stopped) {
            rb.AddForce(0,0, -_lastForceValue);
            _stopped = true;
            return;
        }
        
        // Just in case after game restart
        if (Math.Abs(_lastForceValue) < 1e-3) {
            _lastForceValue = -ForwardForce * Time.deltaTime;
            rb.AddForce(0,0,_lastForceValue);
        }
        
        // Game boost
        if (_gc.isGameBoosted && !_isSpeedBoosted) {
            rb.AddForce(0,0, -_lastForceValue);
            rb.AddForce(0,0, _lastForceValue * ForceMultiplier);
            _lastForceValue *= ForceMultiplier;
            _isSpeedBoosted = true;
        } else if (!_gc.isGameBoosted && _isSpeedBoosted) {
            rb.AddForce(0,0, -_lastForceValue);
            rb.AddForce(0,0, _lastForceValue / ForceMultiplier);
            _lastForceValue /= ForceMultiplier;
            _isSpeedBoosted = false;
        }
    }
}