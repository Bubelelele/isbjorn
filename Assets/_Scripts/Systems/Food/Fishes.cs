using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Fishes : MonoBehaviour
{
    public UnityEvent onHit;
    public Transform player;
    public float pushForce;
    public GameObject icon;
    public Material slashIcon;

    private Rigidbody rb;
    private SphereCollider _sphereCollider;
    private Animator fishAnim;
    private Vector3 dir;
    private bool canRoar;
    private bool isDown;
    private bool isHit;
    private bool canHit, pickUpOnce = false;
    private AudioSource audioSound;

    private GameObject fishInMouth;                 //Kevin
    public Food food;                              //Kevin

    private void Awake()            
    {
        fishInMouth = GameObject.Find("Player/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");            //Kevin
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fishAnim = GetComponent<Animator>();
        audioSound = GetComponent<AudioSource>();

        fishInMouth.SetActive(false);       //Kevin
        food = gameObject.GetComponent<Food>();     //Kevin

    }
    private void Update()
    {
        dir = transform.position - player.position;


        if (canHit)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Invoke("PickUpFish", 0.5f);
                canHit = false; 
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && !isDown && canRoar)
            {  
                rb.isKinematic = false;
                fishAnim.enabled = false;
                rb.AddForce(dir * Time.deltaTime * pushForce);
                isDown = true;
                _sphereCollider.radius = 7.0f;
                icon.GetComponent<MeshRenderer>().material = slashIcon;
                // StartCoroutine(nameof(StopFish));
                
            }
            //if (Mouse.current.leftButton.wasPressedThisFrame && !isHit && isDown)
            //{
            //    fishInMouth.SetActive(true);        //Kevin
            //    fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;      //Kevin
            //    onHit.Invoke();
            //    Destroy(gameObject, 0.2f);
            //    isHit = true;
            //}
            if (!isHit && isDown)           //Kevin
            {
                canHit = true; 
            }
            else                        //Kevin
            {
                canHit = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ground")) return;
        rb.isKinematic = true;
    }

    private IEnumerator StopFish()          //Kevin 
    {
        yield return new WaitForSeconds(3);
        rb.isKinematic = true;
    }

    private void PickUpFish()               //Kevin
    {
        if (!pickUpOnce)
        {
            //Debug.LogError("Pickup!");
            fishInMouth.SetActive(true);        //Kevin
            fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;      //Kevin
            onHit.Invoke();
            Destroy(gameObject, 0.2f);
            isHit = true;
            pickUpOnce = true;
        }
        
    }


    public void CanRoar() { canRoar = true; icon.SetActive(true); }
    public void NoRoar() { canRoar = false; icon.SetActive(false); }
}
