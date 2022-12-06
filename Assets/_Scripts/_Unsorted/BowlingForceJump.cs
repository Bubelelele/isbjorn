using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BowlingForceJump : MonoBehaviour
{
    public float pushForceJump;

    private GameObject player;
    private PlayerStateMachine playerStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); 
        playerStateMachine = player.GetComponent<PlayerStateMachine>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerStateMachine.Input.Rolling)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir = dir.normalized;
            player.GetComponent<Rigidbody>().AddForce((dir * pushForceJump), ForceMode.Impulse);
        }
    }
}
