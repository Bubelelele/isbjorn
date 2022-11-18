using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject deadFish;
    [SerializeField] private GameObject deadFishPrefab;
    [SerializeField] private Transform fishLandingLocation;
    [SerializeField] private AnimationCurve jumpCurve;

    private FishInMouth _fishInMouth;
    private float _respawnTime;
    private float _respawnTimer;

    private void Start()
    {
        _fishInMouth = deadFish.GetComponent<DeadFish>().fishInMouth.GetComponent<FishInMouth>();
        var food = deadFish.GetComponent<Food>();
        _respawnTime = food.howMuchFood / food.playerHunger.hungerOverTime;
        _respawnTimer = _respawnTime;
    }

    private void Update()
    {
        if (!_fishInMouth.hasEaten) return;
        _respawnTimer -= Time.deltaTime;
        if (_respawnTimer >= 0.0f) return;
        Respawn();
         _respawnTimer = _respawnTime;
    }

    private void Respawn()
    {
        // var curveTimer = 1.0f;
        // curveTimer -= Time.deltaTime;
        // jumpCurve.Evaluate(curveTimer);
        Instantiate(deadFishPrefab, fishLandingLocation);
        _fishInMouth.hasEaten = false;
    }
}