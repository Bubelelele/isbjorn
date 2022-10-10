using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRespawnDeactivate : MonoBehaviour
{

    public GameObject firstRespawnPoint;
    public GameObject firstFoodItem;
    private ClosestRespawnFinder cRF;
    private void Start()
    {
        cRF = GameObject.Find("Player").GetComponent<ClosestRespawnFinder>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            firstRespawnPoint.SetActive(false); 
            firstFoodItem.SetActive(false);
            cRF.FindClosestRespawn();
        }
    }
}
