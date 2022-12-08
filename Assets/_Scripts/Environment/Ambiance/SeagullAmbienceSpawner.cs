using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeagullAmbienceSpawner : MonoBehaviour {
    
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Vector2 spawnHeightRange;
    [SerializeField] private Vector2 spawnDistanceRange;
    [SerializeField] private float spawnDeviation;
    [SerializeField] private Vector2 timeBetweenSpawnsRange;
    [SerializeField] private float lifetimeMoveDistance;
    [SerializeField] private float lifetimeMoveTime;

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
        int moveDirection = Mathf.RoundToInt(Random.value) == 0 ? -1 : 1;
        Vector3 moveTarget = _playerTransform.TransformPoint(lifetimeMoveDistance * moveDirection, 0, 0) + spawnPoint;
        spawnedObject.transform.DOMove(moveTarget, lifetimeMoveTime);
        Destroy(spawnedObject, lifetimeMoveTime);
    }

    private Vector3 FindSpawnLocation() {
        for (int i = 0; i < SPAWN_ATTEMPTS; i++) {
            Vector3 pointBehindPlayer = _playerTransform.TransformPoint(0, 
                Random.Range(spawnHeightRange.x, spawnHeightRange.y),
                -Random.Range(spawnDistanceRange.x, spawnDistanceRange.y));
            Vector3 spawnPointToCheck = pointBehindPlayer + Random.insideUnitSphere * spawnDeviation;

            //Check if the sky at this location is not obstructed by anything by shooting a ray from the sky.
            Vector3 rayStart = new Vector3(spawnPointToCheck.x, spawnPointToCheck.y + 200, spawnPointToCheck.z);
            Ray ray = new Ray(rayStart, Vector3.down);
            if (!Physics.Raycast(ray, 250)) {
                return spawnPointToCheck;
            }
        }

        return Vector3.zero;
    }

}
