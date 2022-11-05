using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DisappearingIceFlake : MonoBehaviour
{
    private Animator myAnimation;
    private GameObject parent;
    //public float speed;
    //private Vector3 firstPosition, maxHeight, lowestHeight;


    //public MeshCollider childCollider;

    // Start is called before the first frame update
    void Start()
    {
        //firstPosition = transform.position;
        //maxHeight = new Vector3(0, +2, 0);
        //lowestHeight = new Vector3(0, -10, 0);

        myAnimation = GetComponent<Animator>();
        parent = transform.parent.gameObject; 
        //childCollider = transform.GetChild(0).GetComponent<MeshCollider>();

    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    //Invoke("DeactivateMe", 3f); 
        //    StartCoroutine(Move());

        //}
    }

    void DeactivateMe()
    {
        parent.SetActive(false);

    }

    //IEnumerator Move()
    //{
    //    transform.localPosition = Vector3.Lerp (firstPosition, maxHeight, Time.deltaTime * speed); 
    //    yield return new WaitForSeconds(3);
    //    transform.localPosition = Vector3.Lerp(maxHeight, lowestHeight, Time.deltaTime * speed);


    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        myAnimation.SetBool("PlayerContact", true);
    //        Debug.Log("First");
    //    }

    //    if(collision.gameObject.transform.GetChild(0).name == "Player")
    //    {
    //        myAnimation.SetBool("PlayerContact", true);
    //        Debug.Log("Second");


    //    }


    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.gameObject.CompareTag("Player"))
    //    //{
    //    //    myAnimation.SetBool("PlayerContact", true);

    //    //}

    //    if (other.gameObject.transform.GetChild(0).name == "Player") 
    //    {
    //        myAnimation.SetBool("PlayerContact", true);
    //        Debug.Log("Third");


    //    }
    //}


    public void ActivateAnim()
    {
        myAnimation.SetBool("PlayerContact", true);
        Debug.Log("Child");

    }
}
