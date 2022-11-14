using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BowlingPin : MonoBehaviour
{
    public event Action onPinFelled;
    public bool fallen = false; 
    private void OnEnable()
    {
       // Debug.LogError("HELLO");
    }

    // Start is called before the first frame update
    void Start()
    {
        //onPinFelled?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {   if (fallen) return;
        if (transform.eulerAngles.z > 5 &&
              transform && transform.eulerAngles.z < 355
             )
        {
            
            
                onPinFelled?.Invoke();
                //Debug.LogError("I Fell!");
                fallen = true;
            
        }
    }
    public void resetFall()
    {
        fallen = false; 
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        onPinFelled?.Invoke();
    //        Debug.LogError("Hit!");
    //    }
    //}

}
