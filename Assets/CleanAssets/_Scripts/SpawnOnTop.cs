using System;
using UnityEngine;

public class SpawnOnTop : MonoBehaviour
{
    [SerializeField] private Transform topTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent.position = topTransform.position;
    }
}
