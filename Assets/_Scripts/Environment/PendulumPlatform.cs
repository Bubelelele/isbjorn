using System.Collections.Generic;
using UnityEngine;

public class PendulumPlatform : MonoBehaviour
{
    private Vector3 thisFramePosition;
    private Vector3 lastFramePosition;
    public Vector3 direction;

    [SerializeField] private List<Rigidbody> rigidbodiesOnPlatform = new();

    private void Start()
    {
        thisFramePosition = transform.localPosition;
        lastFramePosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        thisFramePosition = transform.position;

        direction = thisFramePosition - lastFramePosition;

        lastFramePosition = transform.position;

        foreach (var rb in rigidbodiesOnPlatform)
        {
            rb.velocity = direction * 100;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null || other.attachedRigidbody.isKinematic) return;
            if (!rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
                rigidbodiesOnPlatform.Add(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null) return;
            if (rigidbodiesOnPlatform.Contains(other.attachedRigidbody))
                rigidbodiesOnPlatform.Remove(other.attachedRigidbody);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction * 100);
    }

}
