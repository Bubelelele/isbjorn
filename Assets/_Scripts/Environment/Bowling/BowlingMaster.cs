using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BowlingMaster : MonoBehaviour
{
    public int score = 0; 
    public List<BowlingPin> pins;
    public Transform resetPosition;
    public bool scoreIsActive = false;

    public AudioObject strikeSound;     //Mathias
    public AudioObject bowlingFail;     //Mathias
    public GameObject speedVoiceLine;   //Mathias

    private GameObject topDOG;
    private GameObject player;
    private PlayerStateMachine playerStateMachine;
    private GameObject guard;
    private Text bowlingScore;
    private GameObject _bowlScore;

    
    


    Vector3[] positions;

    private void Awake()
    {
        bowlingScore = GameObject.Find("Game Canvas/HUD/BowlingScore").GetComponent<UnityEngine.UI.Text>();
        _bowlScore = GameObject.Find("Game Canvas/HUD/BowlingScore");

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
            pin.onPinFelled += AddScore;
        }
        positions = new Vector3[pins.Count];
        for (int i = 0; i < pins.Count; i++)
        {
            positions[i] = pins[i].transform.position;
        }

        player = GameObject.Find("Player");
        playerStateMachine = player.GetComponent<PlayerStateMachine>();
        topDOG = transform.parent.gameObject;
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
        if (scoreIsActive)
        {
            bowlingScore.text = score + " / 10";

        }
    }
    void CalculateScore()
    {
        if (score == 10)
        {
            playerStateMachine.enabled = true;

            Vocals.instance.Say(strikeSound);     //Mathias

            StartCoroutine(TurnOffWalrusi());
        }
        else
        {
            ResetPins();
            score = 0;
        }
    }

    private IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(5f);
        playerStateMachine.enabled = false;
    }
    private IEnumerator TurnOffWalrusi()
    {
        yield return new WaitForSeconds(1);
        _bowlScore.SetActive(false); 
        Destroy(topDOG);
    }

    public void AddScore()
    {
        score++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerStateMachine.Input.Rolling)
        {
            speedVoiceLine.SetActive(false);
            guard.SetActive(false);
            Invoke("KinematicOff", 1f);

                 StartCoroutine(StopPlayer());
             Invoke("CalculateScore", 8f);
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
        Vocals.instance.Say(bowlingFail); //Mathias
        speedVoiceLine.SetActive(true);   //Mathias
        guard.SetActive(true);
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].transform.position = positions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
            pins[i].resetFall();
            pins[i].GetComponent<Rigidbody>().isKinematic = true;

        }
        player.transform.position = resetPosition.position;
        playerStateMachine.enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
