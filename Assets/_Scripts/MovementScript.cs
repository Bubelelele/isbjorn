using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator bearAnim;

    public float speed = 6f;
    public float runSpeed = 12f;
    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    void Update()
    {
        //Input from movement keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            //Walking animation
            bearAnim.SetBool("IsWalking", true);

            //Rotation angle and smooth turning
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
                bearAnim.SetBool("IsRunning", true);
            }
            else
            {
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                bearAnim.SetBool("IsRunning", false);
            }
            
        }
        else
        {
            //No walking animation
            bearAnim.SetBool("IsWalking", false);
        }

    }
}
