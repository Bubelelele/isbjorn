using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snowball : MonoBehaviour
{
    public GameObject prefabSnowBall;
    public float throwForce;
    private bool contactWithPlayer;





    private void Update()
    {
        if (contactWithPlayer)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Invoke("ThrowBall", 0.3f);
            }
        }
        
    }




    void ThrowBall()
    {
       
        GameObject temp = Instantiate(prefabSnowBall, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody>().AddRelativeForce(Camera.main.transform.forward  * throwForce, ForceMode.Impulse);
        temp.transform.parent = gameObject.transform;
        Destroy(temp, 4); 
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contactWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contactWithPlayer = false;
        }
    }


}
