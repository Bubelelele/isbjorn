using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRespawnDeactivate : MonoBehaviour
{

    public GameObject firstRespawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            firstRespawnPoint.SetActive(false); 
        }
    }
}
