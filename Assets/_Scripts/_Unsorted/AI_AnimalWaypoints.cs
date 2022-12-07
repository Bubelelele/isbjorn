using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_AnimalWaypoints : MonoBehaviour
{
    public string animalNumber;
    public List<Transform> waypoint;
    

    private int _index = 0;
    private NavMeshAgent _agent;
    private GameObject pathOne;


    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
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
        if (!_agent.pathPending && _agent.remainingDistance < 1f)
        {
            GoToNextWaypoint();
        }
    }
}
