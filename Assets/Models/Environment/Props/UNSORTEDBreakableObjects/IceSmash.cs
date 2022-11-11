using UnityEngine;

public class IceSmash : MonoBehaviour, IHittable
{
	public GameObject[] fracturedObject;
	
    private void SpawnFracturedObject()
	{
		fracturedObject[Random.Range(0,3)].SetActive(true);
		gameObject.SetActive(false);
	}
    
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (other.GetComponent<PlayerStateMachine>().Input.RollIsPressed) 
			{
				SpawnFracturedObject();
			}
		}
		else if (other.CompareTag("Cutscene Player"))
		{
			SpawnFracturedObject();
		}
		else if (other.CompareTag("Snowball"))
		{
			SpawnFracturedObject();
		}	
	}

	public void Hit()
	{
		SpawnFracturedObject();
	}
}