using UnityEngine;

public class RollingScript : MonoBehaviour
{
    public float rollingSpeed = 10f;
    public Transform rotationPivot;
    public Animator bearAnim;

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            bearAnim.SetBool("IsRolling", true);
            rotationPivot.Rotate(Vector3.left * -rollingSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            bearAnim.SetBool("IsRolling", false);
            rotationPivot.localRotation = Quaternion.Euler(0,0,0);
        }
        
    }
}
