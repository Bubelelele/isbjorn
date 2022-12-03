using UnityEngine;

public class FishDropLocation : MonoBehaviour
{

    private GameObject fishLocation;

    private void Start()
    {
        fishLocation = this.gameObject.transform.GetChild(0).GetChild(0).gameObject; //Kevin
        // Invoke("FishLocationOff", 2);                                                  //Kevin
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            fishLocation.SetActive(false);
        }
    }

}
