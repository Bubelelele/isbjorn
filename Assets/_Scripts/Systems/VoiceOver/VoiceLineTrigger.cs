using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public AudioObject clipToBePlayed;
	private bool isTriggered;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isTriggered)
		{
			Vocals.instance.Say(clipToBePlayed);
			isTriggered = true;
		}
	}
}
