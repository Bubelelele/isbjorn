using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Respawn : MonoBehaviour
{

    public float maxDistance = 10f;
    public float offset = 2f;
    public Vector3 currentResLoc;

    private ClosestRespawnFinder closestRespawnFinder;

    private Vector3 RespawnLoc;





    public void RespawnPlayer()
    {
        RespawnLoc = new(Random.Range(currentResLoc.x + offset, currentResLoc.x + maxDistance), currentResLoc.y, Random.Range(currentResLoc.z + offset, currentResLoc.z + maxDistance));
        transform.position = RespawnLoc;
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            RespawnPlayer();
        }
    }

}
