using UnityEngine;

public class TeleportMenu : MonoBehaviour
{
    public Transform[] teleportLocations;

    public void Teleport(int section)
    {
        PlayerStateMachine.StaticPlayerTrans.position = teleportLocations[section-1].position;
        PlayerStateMachine.StaticPlayerTrans.rotation = teleportLocations[section-1].rotation;
    }
}
