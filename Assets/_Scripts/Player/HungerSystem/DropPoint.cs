using UnityEngine;

public class DropPoint : MonoBehaviour
{

    private GameObject fishDropPlace;
    private GameObject fishInMouth;


    private void Awake()
    {
        fishInMouth = GameObject.Find("Player/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");
    }
    

    // Update is called once per frame
    void Update()
    {
        FindClosestDropPoint();

        if(fishDropPlace != null)
        {
            if (fishInMouth.activeSelf)
            {
                fishDropPlace.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                fishDropPlace.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void FindClosestDropPoint()
    {
        float distanceToClosestDropPoint = Mathf.Infinity;
        FishDropLocation closestDrop = null;
        FishDropLocation[] allDrops = GameObject.FindObjectsOfType<FishDropLocation>();

        foreach (FishDropLocation currentDropPoint in allDrops)
        {

            float distanceToDropPoint = (currentDropPoint.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToDropPoint < distanceToClosestDropPoint)
            {
                distanceToClosestDropPoint = distanceToDropPoint;
                closestDrop = currentDropPoint;
                fishDropPlace = currentDropPoint.gameObject;
            }
        }

    }
}
