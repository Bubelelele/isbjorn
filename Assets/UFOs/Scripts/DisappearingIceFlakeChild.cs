using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingIceFlakeChild : MonoBehaviour
{
    private DisappearingIceFlake iceFlake;

    // Start is called before the first frame update
    void Start()
    {
        iceFlake = GetComponentInParent<DisappearingIceFlake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        iceFlake.ActivateAnim();
    }

}
