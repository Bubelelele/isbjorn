using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadFish : MonoBehaviour
{
    public Transform player;
    public bool foodInScene, canHit = false;
    public GameObject fishInMouth;                 
    public Food food;  
    
    private Rigidbody _rB;


    private void Awake()
    {
        if (foodInScene)
        {
            food = this.gameObject.GetComponent<Food>();
            _rB = gameObject.GetComponent<Rigidbody>();
        }
        fishInMouth = GameObject.Find("Player/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");           
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        fishInMouth.SetActive(false);      
        
        if (foodInScene)
        {
            StartCoroutine("TurnOffGravity");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.PickUpFish();
            canHit = true;
        }

    }

    private void Update()
    {
        if (canHit)
        {
            IconSystem.instance.PickUpFish();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Invoke("PickUpFish", 0.5f);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        IconSystem.instance.PickUpFish();

    //        if (Mouse.current.leftButton.wasPressedThisFrame)
    //        {
    //            fishInMouth.SetActive(true);        
    //            if (foodInScene)
    //            {
    //                fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;
    //            }
    //            transform.position = Vector3.zero;
    //            Destroy(gameObject, 0.2f);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.TextEnabled(false);
            canHit = false; 
        }
    }

    IEnumerator TurnOffGravity()
    {
        yield return new WaitForSeconds(1f);
        _rB.isKinematic = true;
    }


    private void PickUpFish()
    {
        fishInMouth.SetActive(true);
        if (foodInScene)
        {
            fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;
        }
        transform.position = Vector3.zero;
        Destroy(gameObject, 0.2f);
    }
}
