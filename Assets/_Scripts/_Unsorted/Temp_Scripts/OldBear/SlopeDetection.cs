using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    public LayerMask groudMask;
    public Transform rotatePivot;
    public float interpolation;

    public float targetAngle;
    [HideInInspector] public float currentAngle;

    private RollingScript rollingScript;
    private Vector3 normalPoint;
    private Vector3 normalDirection;
    private Vector3 forwardDirection;
    private float differenceAngle;

    private void Start()
    {
        rollingScript = GetComponent<RollingScript>();
    }
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, groudMask))
        {
            //For Gizmos
            normalPoint = hit.point;
            normalDirection = hit.normal;
            forwardDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

            differenceAngle = Vector3.Angle(normalDirection, forwardDirection);

            targetAngle = 90f - differenceAngle;

            if (currentAngle != targetAngle)
            {
                currentAngle = Mathf.SmoothStep(currentAngle, targetAngle, interpolation * Time.deltaTime);
            }

            if (!rollingScript.isRolling)
            {
                rotatePivot.localRotation = Quaternion.Euler(currentAngle, 0, 0);
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(normalPoint, normalDirection * 5);
        Gizmos.DrawRay(normalPoint, forwardDirection * 5);
    }
}
