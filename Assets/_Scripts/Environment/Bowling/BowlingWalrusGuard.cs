using UnityEngine;

public class BowlingWalrusGuard : MonoBehaviour
{
    public GameObject bowlingMasterObject;
    public float pushForceWalk, pushForceJump, stayTimer;
    public bool inTrigger = false;


    private GameObject player;
    private PlayerStateMachine playerStateMachine;
    private Animator guardAnimation;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<PlayerStateMachine>();
        bowlingMasterObject = transform.parent.gameObject.transform.GetChild(1).gameObject;
        guardAnimation = bowlingMasterObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            stayTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (playerStateMachine.Input.Moving || playerStateMachine.Input.Running)
            {
                Invoke("PushPlayerWalk", 0.5f);
                inTrigger = true;
            }

            if (!playerStateMachine.PlayerIsGrounded)
            {
                Invoke("PushPlayerJump", 0.5f);
                inTrigger = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && stayTimer <= 0)
        {
            PushPlayerWalk();
            stayTimer = 1f; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


    void PushPlayerWalk()
    {
        guardAnimation.SetBool("Enter", true);
        Invoke("AnimReset", 0.3f);
        Vector3 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        player.GetComponent<Rigidbody>().AddForce((dir * pushForceWalk), ForceMode.Impulse);
    }

    void PushPlayerJump()
    {
        guardAnimation.SetBool("Enter", true);
        Invoke("AnimReset", 0.3f);
        Vector3 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        player.GetComponent<Rigidbody>().AddForce((dir * pushForceJump), ForceMode.Impulse);
    }

    void AnimReset()
    {
        guardAnimation.SetBool("Enter", false);
    }
}
