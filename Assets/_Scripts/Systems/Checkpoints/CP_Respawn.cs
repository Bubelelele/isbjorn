using UnityEngine;
using Random = UnityEngine.Random;

public class CP_Respawn : MonoBehaviour
{

    public float maxOffset = 10f;
    public float minOffset = 2f;
    public Vector3 currentResLoc;
    public AudioObject respawnVoiceLine;
    private int voiceLineCounter;

    private ClosestRespawnFinder closestRespawnFinder;

    private Vector3 RespawnLoc;

    private void Start()
    {
        currentResLoc = PlayerStateMachine.StaticPlayerTransform.position;
    }

    public void RespawnPlayer()
    {
        RespawnLoc = new(Random.Range(currentResLoc.x + minOffset, currentResLoc.x + maxOffset), currentResLoc.y, Random.Range(currentResLoc.z + minOffset, currentResLoc.z + maxOffset));
        transform.position = RespawnLoc;
        voiceLineCounter++;
        if (voiceLineCounter == 3)
        {
            Vocals.instance.Say(respawnVoiceLine);
        }
        if (voiceLineCounter == 4)
        {
            voiceLineCounter = 0;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            RespawnPlayer();
        }
    }

}
