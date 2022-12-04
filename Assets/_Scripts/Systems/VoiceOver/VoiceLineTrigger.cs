using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public AudioObject clipToBePlayed;
	public bool onTriggerStay;
	private bool isTriggered;
	private bool waitForAudio = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isTriggered)
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
	


	}
	private IEnumerator Wait()
	{
		
		yield return new WaitForSeconds(10);
		waitForAudio = true;
	}
}
