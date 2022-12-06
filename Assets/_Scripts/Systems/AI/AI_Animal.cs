using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Animal : MonoBehaviour
{
    public string animalNumber;
    public List<Transform> waypoint;
    public bool stationary;//, foodIsDropped;
    public float maxDistanceToFood;

    public float distance;
    public Food _food;
    
    //private bool goToFood;
    private Vector3 closestFoodLocation;
    private int _index = 0;
    private NavMeshAgent _agent;

    private GameObject foodObject;
    private GameObject pathOne;
    private Animator walrusAnim;


    private void Awake()
    {
        //goToFood = false;
        if (!stationary)
        {
            pathOne = GameObject.Find("Paths/PathOne");

            if (animalNumber == ("Walrus1"))
            {
                foreach (Transform child in pathOne.gameObject.transform)
                {
                    waypoint.Add(child.transform);
                }
            }
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
        walrusAnim = GetComponent<Animator>();
    }

    public void GoToNextWaypoint()
    {
        if (waypoint.Count == 0)
            return;

        _agent.SetDestination(waypoint[_index].position);
        _index = (_index + 1) % waypoint.Count;
    }


    // Update is called once per frame
    void Update()
    {
        if (!stationary)
        {
            if (!_agent.pathPending && _agent.remainingDistance < 1f)
            {
                GoToNextWaypoint();
            }
            //transform.LookAt(waypoint[_index].position);
        }

        if (stationary)
        {
            FindClosestFood();
        }
        //if (goToFood)
        //{
        //    _agent.SetDestination(closestFoodLocation);

        //}
        if ((closestFoodLocation - this.transform.position).sqrMagnitude < maxDistanceToFood)
        {
            //if (foodIsDropped)
            if (_food.foodIsDropped)
            {
                _agent.SetDestination(closestFoodLocation);
                walrusAnim.SetBool("Walking", true);
            }

        }
        //float distanceToFood = (currentFood.transform.position - this.transform.position).sqrMagnitude;

        distance = (closestFoodLocation - this.transform.position).sqrMagnitude;
        
        if(distance < 11f)
        {
            IconSystem.instance.TextEnabled(false);
            walrusAnim.SetBool("Walking", false);
            walrusAnim.SetTrigger("Eating");
            _agent.isStopped = true;
            foodObject.SetActive(false);


        }
        else
        {
            _agent.isStopped = false;
        }

    }

    public void JumpedOn()
    {
        walrusAnim.SetTrigger("JumpedOn");    
    }


    private void FindClosestFood()
    {
        float distanceToClosestFood = Mathf.Infinity;
        Food closestFood = null;
        Food[] allFood = GameObject.FindObjectsOfType<Food>();

        foreach (Food currentFood in allFood)
        {
            float distanceToFood = (currentFood.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToFood < distanceToClosestFood)
            {
                distanceToClosestFood = distanceToFood;
                closestFood = currentFood;
                closestFoodLocation = closestFood.transform.position;
                _food = currentFood.GetComponent<Food>();
                foodObject = _food.gameObject;
            }
            //if (distanceToFood < maxDistanceToFood)
            //{
            //    goToFood = true;
            //}
        }

        if (closestFood != null)
        {
            Debug.DrawLine(this.transform.position, closestFood.transform.position);

        }



    }
}
