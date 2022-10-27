using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestFoodFinder : MonoBehaviour
{
    

    public Vector3 closestFoodLocation;

    private void Update()
    {
        FindClosestFood();
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
            }
        }
        Debug.DrawLine(this.transform.position, closestFood.transform.position); 
    }
}
