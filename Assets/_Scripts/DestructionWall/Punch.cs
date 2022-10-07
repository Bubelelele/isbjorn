using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    public GameObject hitArea;

    private void Awake()
    {
        hitArea = GameObject.Find("Player/HitArea");
    }


    // Start is called before the first frame update
    void Start()
    {
        hitArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hitArea.SetActive(true);
            Invoke("DeactivateHitBox", 0.5f);
        }
    }

    void DeactivateHitBox()
    {
        hitArea.SetActive(false);
    }
}
