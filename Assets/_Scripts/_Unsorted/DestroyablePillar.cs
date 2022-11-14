using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePillar : MonoBehaviour
{
    [SerializeField]
    private float myHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (myHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            myHealth--;
        }
    }

}
