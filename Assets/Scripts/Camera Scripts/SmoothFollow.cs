using System.Collections;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    private GameplayController _gc;
    private float _distanceChangingSpeed;
    private float _heightChangingSpeed;
    private float _currentDistance;
    private float _currentHeight;
    private const float ZoomDelay = 0.01f;
    
    [Header("Bounds")]
    [Space]
    
    [SerializeField] private float maxDistance = 6.0f;
    [SerializeField] private float minDistance = 2.5f;
    [SerializeField] private float maxHeight = 5.0f;
    [SerializeField] private float minHeight = 2f;
    
    [Header("Damping")]
    
    [SerializeField] private float heightDamping = 2.0f;
    [SerializeField] private float rotationDamping = 3.0f;
    
    [Space]
    
    [SerializeField] private Transform target;

    private void Awake() {
        _gc = FindObjectOfType<GameplayController>();
        _currentDistance = maxDistance;
        _currentHeight = maxHeight;
        
        // To zoom\unzoom evenly
        _distanceChangingSpeed = (maxDistance - minDistance) / 200f;
        _heightChangingSpeed = (maxHeight - minHeight) / 200f;
    }

    private IEnumerator ZoomIn() {
        while (_currentDistance > minDistance && _currentHeight > minHeight) {
            yield return new WaitForSeconds(ZoomDelay);
            if (_currentDistance > minDistance) _currentDistance -= _distanceChangingSpeed;
            if (_currentHeight > minHeight) _currentHeight -= _heightChangingSpeed;
        }
    }

    private IEnumerator ZoomOut() {
        while (_currentDistance < maxDistance && _currentHeight < maxHeight) {
            yield return new WaitForSeconds(ZoomDelay);
            if (_currentDistance < maxDistance) _currentDistance += _distanceChangingSpeed;
            if (_currentHeight < maxHeight) _currentHeight += _heightChangingSpeed;
        }
    }


    private void LateUpdate() {
        // Early out if we don't have a target
        if (!target) {
            return;
        }

        StartCoroutine(_gc.isGameBoosted ? ZoomIn() : ZoomOut());

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + _currentHeight;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * _currentDistance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }
}