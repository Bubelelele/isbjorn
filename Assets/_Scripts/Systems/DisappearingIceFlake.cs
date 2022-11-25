using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DisappearingIceFlake : MonoBehaviour
{
    private Animator myAnimation;
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {

        myAnimation = GetComponent<Animator>();
        parent = transform.parent.gameObject;

    }



    public void ActivateAnim()
    {
        myAnimation.SetBool("PlayerContact", true);
        Invoke("ResetAnim", 2f);

    }
    private void ResetAnim()
    {
        myAnimation.SetBool("PlayerContact", false);

    }
}
