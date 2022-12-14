using UnityEngine;
using UnityEngine.InputSystem;

public class Icicle_Beahaviour : MonoBehaviour
{
    public float gravityForce = 0.5f;
    public GameObject particleChild;
    private AudioSource icicleSound;
    public bool iCanFall, falling, hasFallen;


    [SerializeField]
    private float waitTime;

    private Rigidbody rB;


    // Start is called before the first frame update
    void Start()
    {
        rB = gameObject.GetComponent<Rigidbody>();
        icicleSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iCanFall && !hasFallen)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                rB.isKinematic = false;
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
            IconSystem.instance.RoarIcicle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            iCanFall = false;
            IconSystem.instance.TextEnabled(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            falling = false;
            hasFallen = true;
            particleChild.SetActive(true);
            rB.isKinematic = true;
            IcicleAudio();
            Invoke("TurnOffPS", 4f + waitTime);
        }
    }

    private void TurnOffPS()
    {
        particleChild.SetActive(false);
    }


    private void ActivateFall()
    {
        falling = true;
        IconSystem.instance.TextEnabled(false);
    }

    private void IcicleAudio()
	{
        icicleSound.Play();
	}
}
