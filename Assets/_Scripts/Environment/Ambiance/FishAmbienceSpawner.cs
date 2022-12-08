using UnityEngine;
using Random = UnityEngine.Random;

public class FishAmbienceSpawner : MonoBehaviour {

    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Vector2 spawnDistanceRange;
    [SerializeField] private Vector2 timeBetweenSpawnsRange;

    //The game will try to find a suitable spawn location, but has a cap on attempts to not search forever.
    private const int SPAWN_ATTEMPTS = 50;
    private Transform _playerTransform;

    private void Start() {
        _playerTransform = FindObjectOfType<PlayerStateMachine>().transform;
        if (_playerTransform == null) Destroy(this);
        QueueNewSpawn();
    }

    private void QueueNewSpawn() {
        float timeUntilSpawn = Random.Range(timeBetweenSpawnsRange.x, timeBetweenSpawnsRange.y);
        Invoke(nameof(SpawnNewObject), timeUntilSpawn);
    }

    private void SpawnNewObject() {
        Vector3 spawnPoint = FindSpawnLocation();
        QueueNewSpawn();
        if (spawnPoint.Equals(Vector3.zero)) return;

        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
        Destroy(spawnedObject, 5f);
    }

    private Vector3 FindSpawnLocation() {
        for (int i = 0; i < SPAWN_ATTEMPTS; i++) {
            //Generate a spawn point which is valid according to the spawnDistanceRange
            Vector2 spawnPointToCheck = Vector2.zero;
            while (spawnPointToCheck.sqrMagnitude == 0) {
                Vector2 randomSpawn = Random.insideUnitCircle * spawnDistanceRange.y;
                if (randomSpawn.sqrMagnitude > Mathf.Pow(spawnDistanceRange.x, 2)) {
                    //randomSpawn is within spawnDistanceRange, so proceed.
                    spawnPointToCheck = randomSpawn;
                    break;
                }
            }

            //Check if the water at this location is not obstructed by anything by shooting a ray from the sky.
            Vector3 rayStart = new Vector3(spawnPointToCheck.x, 400, spawnPointToCheck.y) + _playerTransform.position;
            Ray ray = new Ray(rayStart, Vector3.down);
            if (Physics.Raycast(ray, out var hit, 500)) {
                if (hit.collider.CompareTag("Water")) {
                    return hit.point;
                }
            }
        }

        return Vector3.zero;
    }
}
