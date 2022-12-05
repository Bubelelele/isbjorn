using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballVoiceline : MonoBehaviour
{

    public AudioObject snowballVoiceLine;
    private int snowballCounter;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Snowball"))
		{
            snowballCounter++;
        }
        
        if (other.CompareTag("Snowball") && snowballCounter == 3)
        {
            Vocals.instance.Say(snowballVoiceLine);
            snowballCounter = 0;
        }
    }
}
