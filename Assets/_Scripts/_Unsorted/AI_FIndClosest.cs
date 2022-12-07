using UnityEngine;
using UnityEngine.AI;

public class AI_FindClosest : MonoBehaviour
{
    public float lifeSpan;

    private Vector3 closestFood;
    private ClosestFoodFinder closestFoodFinder;
    private NavMeshAgent smell;
    private Transform foodItem;


    // Start is called before the first frame update
    void Start()
    {
        closestFoodFinder = GameObject.Find("Player").GetComponent<ClosestFoodFinder>();
        smell = GetComponent<NavMeshAgent>();
        closestFood = closestFoodFinder.closestFoodLocation;

    }

    // Update is called once per frame
    void Update()
    {
        smell.SetDestination(closestFood);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            Object.Destroy(this.gameObject, lifeSpan);
        }
    }
    

}
