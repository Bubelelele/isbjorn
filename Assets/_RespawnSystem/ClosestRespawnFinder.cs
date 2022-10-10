using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestRespawnFinder : MonoBehaviour
{
    public Vector3 closestRespawnLocation;
    public PlayerStateMachine playerStateMachine;
    public float timeGrounded = 5f;
    private float groundedTimer; 

    private void Update()
    {

        //TEMP FOR MIDTERM
        if (playerStateMachine.PlayerIsGrounded)
        {
            groundedTimer -= Time.deltaTime;
             if (groundedTimer <= 0)
            {
                FindClosestRespawn();
                groundedTimer = timeGrounded;
            }
        }
        if (!playerStateMachine.PlayerIsGrounded)
        {
            groundedTimer = timeGrounded;
        }






        //FindClosestRespawn();
    }

    private void FindClosestRespawn()
    {
        float distanceToClosestRespawn = Mathf.Infinity;
        RespawnPoint closestRespawn = null;
        RespawnPoint[] allResLoc = GameObject.FindObjectsOfType<RespawnPoint>();

        foreach (RespawnPoint currentRespawnLoc in allResLoc)
        {
            float distanceToResLoc = (currentRespawnLoc.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToResLoc < distanceToClosestRespawn)
            {
                distanceToClosestRespawn = distanceToResLoc;
                closestRespawn = currentRespawnLoc;
                closestRespawnLocation = closestRespawn.transform.position;
            }
        }
        Debug.DrawLine(this.transform.position, closestRespawn.transform.position);
    }
}
