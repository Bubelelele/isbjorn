using UnityEngine;
using Random = UnityEngine.Random;

public class CP_Respawn : MonoBehaviour
{

    public float maxOffset = 10f;
    public float minOffset = 2f;
    public Vector3 currentResLoc;

    private ClosestRespawnFinder closestRespawnFinder;

    private Vector3 RespawnLoc;

    private void Start()
    {
        currentResLoc = PlayerStateMachine.staticPlayerTransform.position;
    }

    public void RespawnPlayer()
    {
        RespawnLoc = new(Random.Range(currentResLoc.x + minOffset, currentResLoc.x + maxOffset), currentResLoc.y, Random.Range(currentResLoc.z + minOffset, currentResLoc.z + maxOffset));
        transform.position = RespawnLoc;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            RespawnPlayer();
        }
    }

}
