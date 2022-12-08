using UnityEngine;

public class TransportPlayer : MonoBehaviour
{
    public GameObject player;

    public void Transport()
    {
        player.transform.position = transform.position;
    }
}
