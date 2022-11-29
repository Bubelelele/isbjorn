using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private UnityEvent onCheckpointTriggered;
    
    private CP_Respawn cp_Respawn;

    // Start is called before the first frame update
    void Start()
    {
        cp_Respawn = FindObjectOfType<CP_Respawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cp_Respawn.currentResLoc = transform.position;
            onCheckpointTriggered.Invoke();
        }
    }
}
