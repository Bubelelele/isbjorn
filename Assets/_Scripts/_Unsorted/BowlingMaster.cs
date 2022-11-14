using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BowlingMaster : MonoBehaviour
{
    public int score = 0; 
    public List<BowlingPin> pins;
    public bool hasEntered;


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
            hasEntered = false;
        }
        else
        {
            //StartCoroutine(ResetPins());
            ResetPins();
            score = 0;
            hasEntered = false;
            

        }
    }

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

    public void AddScore()
    {
        score++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasEntered)
        {
            Invoke("CalculateScore", 5f);
            hasEntered = true; 
        }
    }

    void ResetPins()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            //pins[i].gameObject.SetActive(false);
            pins[i].transform.position = positions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
            pins[i].resetFall();
            //pins[i].gameObject.SetActive(true);
        }
        //Something();
        //Debug.LogError("Something");

        
        //hasEntered = false;
        //score = 0;
    }

    void Something()
    {
        foreach (BowlingPin pin in pins)
        {
            //pins.Add(pin);
            pin.onPinFelled += AddScore;
            //Debug.LogError("Que");
        }
    }
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



}
