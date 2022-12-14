using UnityEngine;

public class CollisonPush : MonoBehaviour
{
    public GameObject bowlingMasterObject;
    public float pushForce;
    public AudioSource walrusPushSound;    //Mathias
    public AudioObject voiceLineWalrus;   //Mathias

    private bool voiceLineIsTriggered;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PushPlayer();
        }
    }


    void PushPlayer()
    {
        walrusPushSound.pitch = Random.Range(0.5f, 1.5f);     //Mathias
        walrusPushSound.Play();     //Mathias
		if (!voiceLineIsTriggered)
		{
            Vocals.instance.Say(voiceLineWalrus);     //Mathias
            voiceLineIsTriggered = true;
        }
        

        guardAnimation.SetBool("Enter", true);
        Invoke("AnimReset", 0.3f);
        Vector3 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        player.GetComponent<Rigidbody>().AddForce((dir * pushForce), ForceMode.Impulse);
    }

    void AnimReset()
    {
        guardAnimation.SetBool("Enter", false);
    }
}
