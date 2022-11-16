
using UnityEngine;

public class BowlingWalrusGuard : MonoBehaviour
{
    public GameObject bowlingMasterObject;
    public float pushForceWalk, pushForceJump;
    private GameObject player;
    private PlayerStateMachine playerStateMachine;
    private Animator guardAnimation;
    public float stayTimer;
    public bool inTrigger = false;


    

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
        if (other.gameObject.CompareTag("Player")) //&& playerStateMachine.Input.MoveIsPressed ) 
        {
            if (playerStateMachine.Input.MoveIsPressed || playerStateMachine.Input.RunIsPressed)
            {
                Invoke("PushPlayerWalk", 0.5f);
                inTrigger = true;

            }

            if (!playerStateMachine.PlayerIsGrounded)
            {
                Invoke("PushPlayerJump", 0.5f);
                inTrigger = true;

            }
            //other.GetComponent<Rigidbody>().AddForce(((transform.position + player.transform.position).normalized * pushForce), ForceMode.Impulse);
            //Vector3 dir = other.con
            //Debug.LogError("PUSH");

            //var dir = (other.transform.position - transform.position).normalized;
            //var rigidBody = other.GetComponent<Rigidbody>();
            //rigidBody.

            //Vector3 dir = other.transform.position - transform.position;
            //dir = dir.normalized;
            //other.GetComponent<Rigidbody>().AddForce((dir * pushForce), ForceMode.Impulse); 
            //Invoke("PushPlayerWalk", 0.5f);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && stayTimer <= 0)
        {
            PushPlayerWalk();
            stayTimer = 1f; 

            //other.GetComponent<Rigidbody>().AddForce(((transform.position + player.transform.position).normalized * pushForce), ForceMode.Impulse);
            //Vector3 dir = other.con
            //Debug.LogError("PUSH");

            //var dir = (other.transform.position - transform.position).normalized;
            //var rigidBody = other.GetComponent<Rigidbody>();
            //rigidBody.

            //Vector3 dir = other.transform.position - transform.position;
            //dir = dir.normalized;
            //other.GetComponent<Rigidbody>().AddForce((dir * pushForce), ForceMode.Impulse); 
            //Invoke("PushPlayer", 0.5f);

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
        //bowlingMasterObject.GetComponent<Animation>().Play("BowlingWalrusGuard");
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
