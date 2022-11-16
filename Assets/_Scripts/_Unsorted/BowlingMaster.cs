using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BowlingMaster : MonoBehaviour
{
    public int score = 0; 
    public List<BowlingPin> pins;
    //public bool hasEntered;
    public Transform resetPosition;
    private GameObject topDOG;

    private GameObject player;
    private PlayerStateMachine playerStateMachine;
    //public BowlingWalrusGuard guard;
    public GameObject guard;


    Vector3[] positions;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            pins.Add(child.gameObject.GetComponent<BowlingPin>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (BowlingPin pin in pins)
        {
            //pins.Add(pin);
            pin.onPinFelled += AddScore;
            //Debug.LogError("Que");
        }
        positions = new Vector3[pins.Count];
        for (int i = 0; i < pins.Count; i++)
        {
            positions[i] = pins[i].transform.position;
        }

        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<PlayerStateMachine>();
        topDOG = transform.parent.gameObject;
        //guard = transform.parent.gameObject.transform.GetChild(2).GetComponent<BowlingWalrusGuard>();
        guard = transform.parent.gameObject.transform.GetChild(2).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            ResetPins();
            score = 0;
        }
        
        //CountPinsDown();
    }
    void CalculateScore()
    {
        if (score == 10)
        {
            Debug.LogError("Win");
            //hasEntered = false;
            playerStateMachine.enabled = true;
            StartCoroutine(TurnOffWalrusi());

        }
        else
        {
            //StartCoroutine(ResetPins());
            ResetPins();
            score = 0;
            //hasEntered = false;
            

        }
    }

    private IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        playerStateMachine.enabled = false;

    }
    private IEnumerator TurnOffWalrusi()
    {
        yield return new WaitForSeconds(1);
        Destroy(topDOG); 
        //for (int i = 0; i < pins.Count; i++)
        //{
        //    Destroy(pins[i].gameObject);
        //    Debug.LogError("TURNOFF");
        //}

    }



    public void AddScore()
    {
        score++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerStateMachine.Input.RollIsPressed) //&& !hasEntered)
        {
            //guard.enabled = false; 
            guard.SetActive(false);
            //for (int i = 0; i < pins.Count; i++) //Walrus falls over when roll
            //{
            //    pins[i].GetComponent<Rigidbody>().isKinematic = false;
            //}
            Invoke("KinematicOff", 1f);

                //other.GetComponent<PlayerStateMachine>().Input.enabled = false;
                //other.GetComponent<PlayerStateMachine>().CurrentState =  ;
                StartCoroutine(StopPlayer());
            //playerStateMachine.enabled = false; 
            Invoke("CalculateScore", 6f);
            //hasEntered = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void KinematicOff()
    {
        for (int i = 0; i < pins.Count; i++) 
        {
            pins[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void ResetPins()
    {
        //guard.enabled = true; 
        guard.SetActive(true);
        for (int i = 0; i < pins.Count; i++)
        {
            //pins[i].gameObject.SetActive(false);
            pins[i].transform.position = positions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
            pins[i].resetFall();
            pins[i].GetComponent<Rigidbody>().isKinematic = true;

            //pins[i].gameObject.SetActive(true);
        }
        //Something();
        //Debug.LogError("Something");


        //hasEntered = false;
        //score = 0;
        player.transform.position = resetPosition.position;
        playerStateMachine.enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    //void Something()                          //IS THIS USED?
    //{
    //    foreach (BowlingPin pin in pins)
    //    {
    //        //pins.Add(pin);
    //        pin.onPinFelled += AddScore;
    //        //Debug.LogError("Que");
    //    }
    //}
    //IEnumerator ResetPins()
    //{
    //    for (int i = 0; i < pins.Count; i++)
    //    {
    //        pins[i].gameObject.SetActive(false);
    //        pins[i].transform.position = positions[i];
    //        pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        pins[i].transform.rotation = Quaternion.identity;

    //    }
    //    for (int i = 0; i < pins.Count; i++)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        pins[i].gameObject.SetActive(true);
    //    }

    //    hasEntered = false;
    //    score = 0;
    //}

    //void CountPinsDown()
    //{
    //    for (int i = 0; i < pins.Count; i++)
    //    {
    //        if (pins[i].transform.eulerAngles.z > 5 &&
    //            transform && pins[i].transform.eulerAngles.z < 355
    //            && pins[i].activeSelf)
    //        {
    //            score++;
    //            pins[i].SetActive(false);
    //            Debug.LogError(score);

    //        }

    //    }
    //}

    //public void CountPinsDown()
    //{
    //    for (int i = 0; i < pins.Count; i++)
    //    {
    //        if (pins[i].transform.eulerAngles.z > 5 &&
    //            transform && pins[i].transform.eulerAngles.z < 355
    //            )
    //        {
    //            Debug.LogError(score);

    //        }

    //    }
    //}

}
