using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCollector : MonoBehaviour {
    private float _distanceBetweenAsteroids = 10f;
    private const float MinDistanceBetweenAsteroids = 5f;
    private const float StartDistance = 50f;
    private const float LeftBound = -3.76f;
    private const float RightBound = 3.76f;
    
    [SerializeField] private GameObject[] asteroids;

    private void Awake() {
        var playerZ = FindObjectOfType<PlayerMovement>().transform.position.z;
        var currentMaxDistance = playerZ + StartDistance - _distanceBetweenAsteroids;
        
        // Spawn asteroids at start
        foreach (var asteroid in asteroids) {
            var temp = asteroid.transform.position;
            currentMaxDistance += _distanceBetweenAsteroids;
            temp.x = Random.Range(LeftBound, RightBound);
            temp.z = currentMaxDistance;
            asteroid.transform.position = temp;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "Road": {
                // Moves road
                var otherTransform = other.transform;
                var temp = otherTransform.position;
                temp.z += other.bounds.size.z * 5;
                otherTransform.position = temp;
                break;
            }
            case "Deadly": {
                var otherTransform = other.transform;
                var temp = otherTransform.position;
                
                //Finds maxDist
                var max = -100f;
                foreach (var asteroid in asteroids) {
                    if (asteroid.transform.position.z > max) {
                        max = asteroid.transform.position.z;
                    }
                }
                
                // Reduce distance between asteroids
                _distanceBetweenAsteroids -= 0.1f;
                if (_distanceBetweenAsteroids < MinDistanceBetweenAsteroids) {
                    _distanceBetweenAsteroids = MinDistanceBetweenAsteroids;
                }
                
                // Move asteroid
                temp.z = max + _distanceBetweenAsteroids;
                temp.x = Random.Range(LeftBound, RightBound);
                otherTransform.position = temp;
                break;
            }
        }
    }
}