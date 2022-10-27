using UnityEngine;

public class Lift : MonoBehaviour
{
    public Transform[] liftDestinations;
    public GameObject platform;
    public float waitTime;
    public float speed;

    private float timer;
    private int liftNumber;

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            if(Vector3.Distance(platform.transform.position, liftDestinations[liftNumber].position) > 0.1f)
            {
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, liftDestinations[liftNumber].position, speed * Time.deltaTime);
            }
            else
            {
                timer = 0;
                if (liftNumber >= liftDestinations.Length - 1)
                {
                    liftNumber = 0;
                }
                else
                {
                    liftNumber++;
                }
            }
        }
        
    }

}
