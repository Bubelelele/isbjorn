using UnityEngine;

public class IconLookAtCam : MonoBehaviour
{
    public GameObject cam;

    private void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
