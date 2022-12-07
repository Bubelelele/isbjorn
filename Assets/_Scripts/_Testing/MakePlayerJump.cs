using UnityEngine;

public class MakePlayerJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerStateMachine>().LandedOnWalrus = true;
        GetComponentInParent<AI_Animal>().JumpedOn();
    }
}
