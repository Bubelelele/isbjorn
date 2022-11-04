using UnityEngine;
using Cinemachine;

public class RollingScript : MonoBehaviour
{
    [Header("References")] 
    public Transform rotationPivot;
    public Animator bearAnim;
    public Transform cam;
    public CinemachineFreeLook cinemachineFreeLook;

    [Header("Values")]
    public float maxSlopeBeforeDecelaration = 5;
    public float turnSmoothTime = 0.1f;
    public float accelerationForce = 10f;
    public float decelerationForce = 10f;
    public float jumpBoost = 10f;
    public float animationMultiplier = 10f;
    public float camSpeed = 10f;

    [HideInInspector]public bool isRolling;

    private CharacterController controller;
    private Vector3 moveDir;
    private Vector3 prevMoveDir;
    private MovementScript movementScript;
    private SlopeDetection slopeDetection;
    private JumpingScript jumpingScript;

    private float normalCinemachineCamSpeed;
    private float turnSmoothVelocity;
    private float rollingSpeed;

    private bool UpSlope;
    private bool leap;
    private bool camBack;

    private void Start()
    {
        jumpingScript = GetComponent<JumpingScript>();
        movementScript = GetComponent<MovementScript>();
        controller = GetComponent<CharacterController>();
        slopeDetection = GetComponent<SlopeDetection>();
        normalCinemachineCamSpeed = cinemachineFreeLook.m_XAxis.m_MaxSpeed;
    }

    void Update()
    {
        moveDir = movementScript.moveDir;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            isRolling = true;
            camBack = false;
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0.05f;
            cinemachineFreeLook.m_Lens.FieldOfView = Mathf.SmoothStep(cinemachineFreeLook.m_Lens.FieldOfView, 60, camSpeed * Time.deltaTime);
            bearAnim.SetBool("IsRolling", true);

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(slopeDetection.targetAngle > maxSlopeBeforeDecelaration)
            {
                rollingSpeed += slopeDetection.targetAngle * accelerationForce * Time.deltaTime;
                
            }
            else if(slopeDetection.targetAngle <= maxSlopeBeforeDecelaration && rollingSpeed > 0f)
            {
                rollingSpeed -= 1 * decelerationForce * Time.deltaTime;              
                
            }
            else
            {
                rollingSpeed = 0f;
            }
            
            
            //When you hit a slope
            if(slopeDetection.targetAngle < 0 && !UpSlope)
            {
                prevMoveDir = moveDir;
                Debug.Log("1");
                UpSlope = true;
                
            }
            if(slopeDetection.targetAngle >= 0 && UpSlope && !jumpingScript.isGrounded)
            {
                Debug.Log("2");
                UpSlope = false;
                leap = true;
                rollingSpeed += jumpBoost;
                
            }
            if (leap)
            {
                Debug.Log("leap");
                moveDir = new Vector3(moveDir.x, prevMoveDir.y + 0.5f, moveDir.z);
                Invoke("LeapDone", 0.2f);

            }

            controller.Move(moveDir.normalized * rollingSpeed * Time.deltaTime);
            rotationPivot.Rotate(Vector3.left, -rollingSpeed * animationMultiplier * Time.deltaTime, Space.Self);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isRolling= false;
            camBack = true;
            rollingSpeed = 0f;
            bearAnim.SetBool("IsRolling", false);
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = normalCinemachineCamSpeed;
            
        }

        if (camBack)
        {
            cinemachineFreeLook.m_Lens.FieldOfView = Mathf.SmoothStep(cinemachineFreeLook.m_Lens.FieldOfView, 40, camSpeed * Time.deltaTime);
        }
    }
    private void LeapDone()
    {
        leap = false;
        prevMoveDir = moveDir;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, moveDir * 5);
    }
}
