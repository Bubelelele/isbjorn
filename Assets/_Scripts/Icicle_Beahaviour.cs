 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle_Beahaviour : MonoBehaviour
{
    public BoxCollider _bCGroundCollider;
    public float gravityForce = 0.5f;
    public GameObject particleChild;


    [SerializeField]
    private float waitTime;

    private Rigidbody rB;
    private bool iCanFall, falling, hasFallen;


    // Start is called before the first frame update
    void Start()
    {
        rB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iCanFall && !hasFallen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //falling = true;
                Invoke("ActivateFall", waitTime);
            }
        }

        if (falling)
        {
            rB.AddForce(0, -gravityForce, 0);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iCanFall = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iCanFall = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            falling = false;
            hasFallen = true;
            particleChild.SetActive(true); 
        }
    }

    private void ActivateFall()
    {
        falling = true;

    }


}
