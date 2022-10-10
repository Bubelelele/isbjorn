using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public float maxDistance = 10f;
    public float offset = 2f;
    public Vector3 RespawnLoc;

    private ClosestRespawnFinder closestRespawnFinder;

    private Vector3 currentResLoc;


    // Start is called before the first frame update
    void Start()
    {
        closestRespawnFinder = GameObject.Find("Player").GetComponent<ClosestRespawnFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        currentResLoc = closestRespawnFinder.closestRespawnLocation;


        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    RespawnPlayer();
        //}

    }


    public void RespawnPlayer()
    {
        RespawnLoc = new (Random.Range(currentResLoc.x + offset, currentResLoc.x + maxDistance), currentResLoc.y, Random.Range(currentResLoc.z + offset, currentResLoc.z + maxDistance));
        transform.position = RespawnLoc;
    }



    //TEMP FOR MIDTERM

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            RespawnPlayer();
        }
    }

}
