using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearCTRL : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotSpeed = 0.005f;


    [SerializeField] Transform target;

    private CharacterController charController;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            movement = (right * horInput) + (forward * vertInput);

            movement = (right * horInput) + (forward * vertInput);
            movement *= movementSpeed;
            movement = Vector3.ClampMagnitude(movement, movementSpeed);

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);



        }

        movement *= Time.deltaTime;
        charController.Move(movement);

    }
}
