using UnityEngine;

public class IceSmash : PlayerStateMachine
{

	public GameObject originalGameObject;
	public GameObject[] fracturedObject;
	public float originalSpawnDelay;
	private bool isPressed;



    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        {
		//	SpawnFracturedObject();
        }
		
    }
    public void SpawnFracturedObject()
	{

		if (!isPressed)
		{
			originalGameObject.SetActive(false);
			
			fracturedObject[Random.Range(0,3)].SetActive(true);


			isPressed = true;
		}


	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			if (IsRolling)
			{
				SpawnFracturedObject();
			}
		}
	}

}