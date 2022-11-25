using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuoyRandomizeRotation : MonoBehaviour {
    private void Start() {
        transform.Rotate(0, Random.Range(0, 360f), 0);
    }
}
