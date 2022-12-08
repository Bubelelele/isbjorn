using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int health;


    //[Header("ExposionForce")]
    //public float radius = 5f;
    //public float power = 10f;
    


    [SerializeField]
    private Transform pfWallBroken;


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        Damage(1);
    //    }

        
    //}

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        //Vector3 explosionPos = transform.position;
        //Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        Transform wallBrokenTransform = Instantiate(pfWallBroken, transform.position, transform.rotation);
        //foreach (Transform child in wallBrokenTransform)            //Doesn't add explosionforce to children....
        //{
        //    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
        //    {
        //        childRigidbody.AddExplosionForce(100f, transform.forward, 5f);
        //    }
        //}


        //Did not work

        //foreach (Transform child in wallBrokenTransform)
        //{

        //        Rigidbody rb = child.GetComponent<Rigidbody>();
        //        if (rb != null)
        //        {
        //            rb.AddExplosionForce(power, explosionPos, radius, 3f);
        //        }

        //}


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            Damage(1);
            //Debug.Log("Damage");
        }
    }


}
