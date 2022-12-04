using UnityEngine;

public class IceSmash : MonoBehaviour, IHittable
{
    public GameObject[] fracturedObject;
    public bool onTouch;
    public AudioSource iceShatterSound;
    public bool section2SlideIce;

    [Header("Snowball Section")] //Kev
    public bool snowball;       // Kev
    private SnowballSection snowballSection;    //Kev
    
    private void Start()
    {
        if (snowball)   //Kev
        {
            snowballSection = this.GetComponentInParent<SnowballSection>(); //Kev
        }
    }

    private void SpawnFracturedObject()
    {
        if (section2SlideIce)
        {
            iceShatterSound.Play();
        }
		else
		{
            iceShatterSound.pitch = Random.Range(0.9f, 1.1f);
            iceShatterSound.Play();
        }
        
        if (snowball)   //Kev
        {
            snowballSection.score++;    //Kev
        }
       

        fracturedObject[Random.Range(0, 3)].SetActive(true);
        gameObject.SetActive(false);

		
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerStateMachine>().Input.RollIsPressed)
            {
                SpawnFracturedObject();
            }
            if (onTouch)
            {
                SpawnFracturedObject();
            }
        }
        else if (other.CompareTag("Cutscene Player"))
        {
            SpawnFracturedObject();
        }
        else if (other.CompareTag("Snowball"))
        {
            SpawnFracturedObject();
        }

    }

    public void Hit()
    {
        SpawnFracturedObject();
    }
}