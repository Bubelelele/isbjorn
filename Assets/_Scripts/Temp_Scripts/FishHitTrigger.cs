using _Scripts.Input;
using UnityEngine.Events;
using UnityEngine;

public class FishHitTrigger : MonoBehaviour
{
    public PlayerInput input;
    public UnityEvent onHit;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (input.Slashing)
            {
                onHit.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
