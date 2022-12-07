using UnityEngine;
using System;

public class BowlingPin : MonoBehaviour
{
    public event Action onPinFelled;
    public bool fallen = false; 


    // Update is called once per frame
    void Update()
    {   if (fallen) return;
        if (transform.eulerAngles.z > 5 && transform && transform.eulerAngles.z < 355)
        {
                onPinFelled?.Invoke();
                fallen = true;
        }
    }
    public void resetFall()
    {
        fallen = false; 
    }

}
