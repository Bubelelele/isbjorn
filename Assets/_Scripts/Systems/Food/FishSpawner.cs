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
    private bool timerCanStart;

    private void Start()
    {
        _fishInMouth = deadFish.GetComponent<DeadFish>().fishInMouth.GetComponent<FishInMouth>();
        var food = deadFish.GetComponent<Food>();
        _respawnTime = food.howMuchFood / food.playerHunger.hungerOverTime;
        _respawnTimer = _respawnTime;
    }

    private void Update()
    {
        if (fishLandingLocation.childCount == 0)
        {
            if (_fishInMouth.hasEaten)
            {
                timerCanStart = true; 
            }
        }

        if (timerCanStart)
        {
            _respawnTimer -= Time.deltaTime;
            _fishInMouth.hasEaten = false;

            if (_respawnTimer >= 0.0f) return;
            Respawn();

        }
        
    }

    private void Respawn()
    {
        // var curveTimer = 1.0f;
        // curveTimer -= Time.deltaTime;
        // jumpCurve.Evaluate(curveTimer);

        Instantiate(deadFishPrefab, (fishLandingLocation.position + new Vector3(0,1,0)), Quaternion.identity, fishLandingLocation);
        
        deadFish = deadFishPrefab;
        _respawnTimer = _respawnTime;
        timerCanStart = false; 
    }
}