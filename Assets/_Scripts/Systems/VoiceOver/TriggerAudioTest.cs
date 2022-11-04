using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioTest : MonoBehaviour
{
    public AudioObject clipToBePlayed;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Vocals.instance.Say(clipToBePlayed);
		}
	}
}
