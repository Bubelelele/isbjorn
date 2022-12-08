using UnityEngine;

public class JumpingScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groudMask;
    public Animator bearAnim;

    public float groundDistance = 0.4f;
    public float gravity = -60f;
    public float jumpHeight = 3f;

    private Vector3 velocity;
    public bool isGrounded;


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groudMask);

        //Landing
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            bearAnim.SetBool("IsFalling", false);
            bearAnim.SetBool("IsJumping", false);
        }

        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            bearAnim.SetBool("IsJumping", true);
            bearAnim.SetBool("IsFalling", true);
        }
        //Falling
        else if (!isGrounded)
        {
            bearAnim.SetBool("IsFalling", true);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
