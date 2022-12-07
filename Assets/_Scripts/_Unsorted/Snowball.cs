using UnityEngine;
using UnityEngine.InputSystem;

public class Snowball : MonoBehaviour
{
    public GameObject prefabSnowBall;
    public float throwForce, waitTimer;

    private bool contactWithPlayer;

    private void Update()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = 0;
        }

        if (contactWithPlayer)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && waitTimer <= 0)
            {
                Invoke("ThrowBall", 0.3f);
                waitTimer = 2;
            }
        }
    }




    void ThrowBall()
    {
       
        GameObject temp = Instantiate(prefabSnowBall, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody>().AddRelativeForce(Camera.main.transform.forward  * throwForce, ForceMode.Impulse);
        temp.transform.parent = gameObject.transform;
        Destroy(temp, 4); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contactWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            contactWithPlayer = false;
        }
    }
}
