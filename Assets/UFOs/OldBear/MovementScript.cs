using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Animator bearAnim;

    [Header("Values")]
    public float speed = 6f;
    public float runSpeed = 12f;
    public float turnSmoothTime = 0.1f;
    public float testNumber;

    [HideInInspector] public Vector3 moveDir;

    private CharacterController controller;
    private SlopeDetection slopeDetection;
    private RollingScript rollingScript;
    private float turnSmoothVelocity;
    private float horizontal;
    private float vertical;
    
    private void Start()
    {
        slopeDetection = GetComponent<SlopeDetection>();
        rollingScript = GetComponent<RollingScript>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!rollingScript.isRolling)
        {
            //Input from movement keys
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Rotation angle and smooth turning
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        //Movement direction
        moveDir = Quaternion.Euler(slopeDetection.currentAngle, targetAngle, 0f) * Vector3.forward;

        if (direction.magnitude >= 0.1f && !rollingScript.isRolling)
        {
            //Walking animation
            bearAnim.SetBool("IsWalking", true);           
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Running animation
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
