using UnityEngine;

public class DeactivateFishQuest : MonoBehaviour
{
    public GameObject fishCanvas;

    private void OnTriggerEnter(Collider other)
    {
        fishCanvas.SetActive(false); 
    }
}
