using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuoyRandomizeCycleOffset : MonoBehaviour {
    private static readonly int property = Animator.StringToHash("Cycle Offset");

    private void Start() {
        GetComponent<Animator>().SetFloat(property, Random.Range(0, 1f));
    }
}
