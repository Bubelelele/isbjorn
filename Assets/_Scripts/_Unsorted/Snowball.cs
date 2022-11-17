using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snowball : MonoBehaviour
{
    public GameObject prefabSnowBall;
    public float throwForce;
    private bool contactWithPlayer;
    public float waitTimer;

    //private Animator playerAnim;
    //AnimatorStateInfo animStateInfo;
    //private float nTime;

    //private bool animationFinished;

    //private void Start()
    //{
    //    playerAnim = GameObject.Find("Player/RollPivot/Bear_Big").GetComponent<Animator>();
    //}


    private void Update()
    {
        //if (playerAnim.GetCurrentAnimatorStateInfo().IsName(""))
        //{
        //    playerAnim.
        //}

        //animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(8);
        //nTime = animStateInfo.normalizedTime;

        //if (nTime > 1.0f)
        //{
        //    animationFinished = true;
        //    Debug.LogError("Slash Finished");
        //}


        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = 0;
        }

        if (contactWithPlayer)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && waitTimer <= 0)
            {
                Invoke("ThrowBall", 0.3f);
                waitTimer = 2;
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
