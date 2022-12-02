using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadFish : MonoBehaviour
{
    public Transform player;
         
    public GameObject fishInMouth;                 
    public Food food;                              
    private Rigidbody _rB;

    private void Awake()
    {
        food = this.gameObject.GetComponent<Food>();    
        fishInMouth = GameObject.Find("Player/RollPivot/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");           
    }

    private void Start()
    {

        player = GameObject.Find("Player").transform;
        fishInMouth.SetActive(false);      
        

        _rB = gameObject.GetComponent<Rigidbody>();
        StartCoroutine("TurnOffGravity");



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.PickUpFish();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && Vector3.Distance(transform.position, player.transform.position) < 3)
            {
                IconSystem.instance.FishInMouth();
                fishInMouth.SetActive(true);        
                fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;      
                Destroy(gameObject, 0.2f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.TextEnabled(false);
        }
    }

    IEnumerator TurnOffGravity()
    {
        yield return new WaitForSeconds(1f);
        _rB.isKinematic = true;
    }
}
