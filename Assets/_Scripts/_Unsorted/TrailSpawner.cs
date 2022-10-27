using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{

    //public float timeToSpawn;
    //private float spawnTimer;

    public Transform spawnLoc;
    public GameObject trailPrefab;
    public GameObject prefabClone;

    // Start is called before the first frame update
    void Start()
    {
        //spawnTimer = timeToSpawn; 
    }

    // Update is called once per frame
    void Update()
    {
        //spawnTimer -= Time.deltaTime;
        spawnLoc = GameObject.Find("Player").transform;

        //if ( spawnTimer <= 0)
        //{
        //    Instantiate(trailPrefab, spawnLoc.position, transform.rotation);
        //    spawnTimer = timeToSpawn;
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (prefabClone != null)
            {
                Destroy(prefabClone);
            }
               prefabClone = Instantiate(trailPrefab, spawnLoc.position, transform.rotation);
            

        }

    }
}
