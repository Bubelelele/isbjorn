
using UnityEngine;

public class BowlingGameStart : MonoBehaviour
{
    private BoxCollider bowlingCollider;
    private GameObject bowlingScore;
    private BowlingMaster bowlingMaster;

    // Start is called before the first frame update
    void Start()
    {
        bowlingCollider = transform.parent.gameObject.transform.GetChild(1).GetComponent<BoxCollider>();
        bowlingCollider.enabled = false;
        bowlingScore = GameObject.Find("Game Canvas/BowlingScore");
        bowlingScore.SetActive(false);
        bowlingMaster = GameObject.Find("3- Bowling/Bowling Walrus/BowlingMaster").GetComponent<BowlingMaster>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bowlingCollider.enabled = true;
            bowlingMaster.scoreIsActive = true; 
            bowlingScore.SetActive(true); 
            Destroy(this.gameObject); 
        }
    }

}
