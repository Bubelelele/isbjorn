using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private AnimationCurve positionOverTime;
    [SerializeField] private List<Rigidbody> rigidbodiesOnPlatform = new();
    [SerializeField] private List<CharacterController> characterControllersOnPlatform = new();
    private Rigidbody _platformRigidbody;
    private Vector3 _startPosition;
    private GameObject _destinationPlatform;
    private Vector3 _endPosition;
    private Vector3 _platformPositionLastFrame;

    private void Awake()
    {
        _platformRigidbody = GetComponent<Rigidbody>();
        _startPosition = _platformRigidbody.position;
        _destinationPlatform = transform.parent.GetChild(1).gameObject;
        _endPosition = _destinationPlatform.transform.position;
        _destinationPlatform.SetActive(false);
    }

    private void FixedUpdate()
    {
        _platformPositionLastFrame = _platformRigidbody.position;
        _platformRigidbody.position = Vector3.Lerp(_startPosition, _endPosition, positionOverTime.Evaluate(Time.fixedTime));
        foreach (var rb in rigidbodiesOnPlatform)
        {
            MoveRigidbodiesOnPlatform(rb);
        }
        foreach (var characterController in characterControllersOnPlatform)
        {
            MoveCharacterControllersOnPlatform(characterController);
        }
    }

    private void MoveRigidbodiesOnPlatform(Rigidbody rb)
    {
        rb.position += _platformRigidbody.position - _platformPositionLastFrame;
    }
    
    private void MoveCharacterControllersOnPlatform(Component characterController)
    {
        characterController.transform.position += _platformRigidbody.position - _platformPositionLastFrame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null && other.GetComponent<CharacterController>() == null) return;
        if (!rigidbodiesOnPlatform.Contains(other.attachedRigidbody) && !other.GetComponent<CharacterController>().enabled)
            rigidbodiesOnPlatform.Add(other.attachedRigidbody);
        if (!characterControllersOnPlatform.Contains(other.GetComponent<CharacterController>()) && other.GetComponent<CharacterController>().enabled)
            characterControllersOnPlatform.Add(other.GetComponent<CharacterController>());
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null && other.GetComponent<CharacterController>() == null) return;
        if (rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
            rigidbodiesOnPlatform.Remove(other.attachedRigidbody);
        if (characterControllersOnPlatform.Contains(other.GetComponent<CharacterController>()) && !other.GetComponent<CharacterController>().enabled)
            characterControllersOnPlatform.Remove(other.GetComponent<CharacterController>());
    }
}