using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private AnimationCurve positionOverTime;
    [SerializeField] private List<Rigidbody> rigidbodiesOnPlatform = new();
    private Rigidbody _platformRigidbody;
    private Vector3 _startPosition;
    private GameObject _destinationPlatform;
    private Vector3 _endPosition;
    private Vector3 _platformPositionLastFrame;
    private bool _playerIsOnPlatform;

    private void Awake()
    {
        _platformRigidbody = GetComponent<Rigidbody>();
        _startPosition = _platformRigidbody.position;
        _destinationPlatform = transform.parent.GetChild(1).gameObject;
        _endPosition = _destinationPlatform.transform.position;
        _destinationPlatform.SetActive(false);
    }

    private void Update()
    {
        _platformPositionLastFrame = _platformRigidbody.position;
        _platformRigidbody.position = Vector3.Lerp(_startPosition, _endPosition, positionOverTime.Evaluate(Time.time));
        foreach (var rb in rigidbodiesOnPlatform)
        {
            MoveRigidbodiesOnPlatform(rb);
        }
    }

    private void MoveRigidbodiesOnPlatform(Rigidbody rb)
    {
        rb.position += _platformRigidbody.position - _platformPositionLastFrame;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        if (!rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
            rigidbodiesOnPlatform.Add(other.attachedRigidbody);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        if (rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
            rigidbodiesOnPlatform.Remove(other.attachedRigidbody);
    }
}