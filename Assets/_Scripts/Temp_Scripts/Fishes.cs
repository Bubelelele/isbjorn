using _Scripts.Input;
using UnityEngine.Events;
using UnityEngine;

public class Fishes : MonoBehaviour
{
    public UnityEvent onHit;
    public PlayerInput input;
    public Transform player;
    public float pushForce;

    private Rigidbody rb;
    private Animator fishAnim;
    private Vector3 dir;
    private bool canRoar;
    private bool isDown;
    private bool isHit;
    private AudioSource audioSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fishAnim = GetComponent<Animator>();
        audioSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        dir = transform.position - player.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (input.Roaring && !isDown && canRoar)
            {  
                rb.isKinematic = false;
                fishAnim.enabled = false;
                rb.AddForce(dir * Time.deltaTime * pushForce);
                isDown = true;
                
            }
            if (input.Slashing && Vector3.Distance(transform.position, player.transform.position) < 3 && !isHit && isDown)
            {
                onHit.Invoke();
                Destroy(gameObject, 0.2f);
                isHit = true;
            }
        }
    }
    public void CanRoar() { canRoar = true; }
    public void NoRoar() { canRoar = false; }

    private void FishSound()
	{
        audioSound.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Water"))
		{
            FishSound();
		}
	}
}
