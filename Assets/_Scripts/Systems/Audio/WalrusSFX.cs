using UnityEngine;

public class WalrusSFX : MonoBehaviour
{
    public AudioSource walrusSFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            walrusSFX.pitch = Random.Range(0.9f, 1.1f);
            walrusSFX.Play();
        }
    }
}
