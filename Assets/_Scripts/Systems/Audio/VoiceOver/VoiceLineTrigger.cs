using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
	public GameObject fishInMouth;
    public AudioObject clipToBePlayed;
	public bool onTriggerStay;
	public bool notOnTriggerEnter;
	public bool fishOnTriggerStay;
	private bool isTriggered;
	private bool waitForAudio = true;

    private void Awake()
    {
		fishInMouth = GameObject.Find("Player/Bear_Big/Spine/Spine2/Spine3/Spine4/Neck1/Neck2/Jaw1/FishInMouth");
	}
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isTriggered && !notOnTriggerEnter)
		{
			Vocals.instance.Say(clipToBePlayed);
			isTriggered = true;
		}
		
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && onTriggerStay && waitForAudio)
		{
			Vocals.instance.Say(clipToBePlayed);
			
			StartCoroutine("Wait", 0);
			waitForAudio = false;
		}
        if (other.CompareTag("Player") && fishOnTriggerStay && fishInMouth.activeSelf == true && !isTriggered)
        {
          
			Vocals.instance.Say(clipToBePlayed);
			isTriggered = true;
        }
	}
	private IEnumerator Wait()
	{
		
		yield return new WaitForSeconds(10);
		waitForAudio = true;
	}
}
