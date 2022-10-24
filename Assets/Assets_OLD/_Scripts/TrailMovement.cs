using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMovement : MonoBehaviour
{
    public float speed;

    private Vector3 target;



    private ClosestFoodFinder ClosestFoodFinder;

    // Start is called before the first frame update
    void Start()
    {
        ClosestFoodFinder = GameObject.Find("Player").GetComponent<ClosestFoodFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        target = ClosestFoodFinder.closestFoodLocation;

        
        
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step); 


        

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject, 2f) ; 
        }
        else
        {
            Destroy(gameObject, 5f);
        }

    }
}
