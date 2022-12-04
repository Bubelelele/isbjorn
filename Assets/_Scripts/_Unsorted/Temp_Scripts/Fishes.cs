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
    private Animator fishAnim;
    private Vector3 dir;
    private bool canRoar;
    private bool isDown;
    private bool isHit;
    private AudioSource audioSound;

    private GameObject fishInMouth;                 //Kevin
    public Food food;                              //Kevin

    private void Awake()            
    {
        fishInMouth = GameObject.Find("Player/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");            //Kevin
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
                icon.GetComponent<MeshRenderer>().material = slashIcon;
                StartCoroutine("stopFish");
                
            }
            if (Mouse.current.leftButton.wasPressedThisFrame && Vector3.Distance(transform.position, player.transform.position) < 7 && !isHit && isDown)
            {
                fishInMouth.SetActive(true);        //Kevin
                fishInMouth.GetComponent<FishInMouth>().foodAmount = food.howMuchFood;      //Kevin
                onHit.Invoke();
                Destroy(gameObject, 0.2f);
                isHit = true;
            }
        }
    }

    IEnumerator stopFish()          //Kevin 
    {
        yield return new WaitForSeconds(3);
        rb.isKinematic = true;
    }


    public void CanRoar() { canRoar = true; icon.SetActive(true); }
    public void NoRoar() { canRoar = false; icon.SetActive(false); }
}
