using UnityEngine;

public class FishDropLocation : MonoBehaviour
{

    public GameObject fishLocation;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            fishLocation.SetActive(false);
        }
    }
}
