using System;
using System.Collections.Generic;
using UnityEngine;

public class ParentToLift : MonoBehaviour
{
    private Rigidbody _platformRigidbody;
    private Vector3 _platformPositionLastFrame;

    [SerializeField] private List<Rigidbody> rigidbodiesOnPlatform = new List<Rigidbody>();

    private void Start()
    {
        _platformRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _platformPositionLastFrame = _platformRigidbody.position;
        foreach (Rigidbody rigidbody in rigidbodiesOnPlatform)
        {
            MoveRigidbodiesOnPlatform(rigidbody);
        }
    }

    private void MoveRigidbodiesOnPlatform(Rigidbody rigidbody)
    {
        rigidbody.position += _platformRigidbody.position - _platformPositionLastFrame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            if (!rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
                rigidbodiesOnPlatform.Add(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            if (rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
                rigidbodiesOnPlatform.Remove(other.attachedRigidbody);
        }
    }
}