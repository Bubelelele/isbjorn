
using UnityEngine;

public class BowlingGameStart : MonoBehaviour
{
    private BoxCollider bowlingCollider;

    // Start is called before the first frame update
    void Start()
    {
        bowlingCollider = transform.parent.gameObject.transform.GetChild(1).GetComponent<BoxCollider>();
        bowlingCollider.enabled = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bowlingCollider.enabled = true;
            Destroy(this.gameObject); 
        }
    }

}
