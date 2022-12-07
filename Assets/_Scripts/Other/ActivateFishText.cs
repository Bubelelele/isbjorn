using UnityEngine;

public class ActivateFishText : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.PickUpFish();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IconSystem.instance.TextEnabled(false);
        }
    }
}
