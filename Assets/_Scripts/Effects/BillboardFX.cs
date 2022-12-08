using UnityEngine;

public class BillboardFX : MonoBehaviour {
    
    private Transform _camTransform;
    private Quaternion _originalRotation;

    void Start() {
        var cam = Camera.main;
        _camTransform = cam.transform;
        _originalRotation = transform.rotation;
    }

    void Update() {
        if (!_camTransform.gameObject.activeSelf) return;
        transform.rotation = _camTransform.rotation * _originalRotation;
    }
}