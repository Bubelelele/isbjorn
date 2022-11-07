using UnityEngine;

public class IceSmash : MonoBehaviour
{
	public GameObject originalGameObject;
	public GameObject[] fracturedObject;
	public float originalSpawnDelay;

	private bool isPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
			SpawnFracturedObject();
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

}