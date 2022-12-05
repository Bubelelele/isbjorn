using UnityEngine;
using Cinemachine;

public class ContinuousImpulse : MonoBehaviour {
    
    [field: SerializeField] public bool Active { get; set; }
    [SerializeField] private Vector3 impulseVector = Vector3.one;
    
    [CinemachineImpulseDefinitionProperty] [SerializeField]
    private CinemachineImpulseDefinition impulseDefinition = new();

    private float _lastEventTime;
    private float _impulseStrength = 1;

    private void Update() {
        var now = Time.time;
        var eventLength = impulseDefinition.m_TimeEnvelope.m_AttackTime +
                          impulseDefinition.m_TimeEnvelope.m_SustainTime;
        if (Active && now - _lastEventTime > eventLength) {
            impulseDefinition.CreateEvent(transform.position, impulseVector * _impulseStrength);
            _lastEventTime = now;
        }
    }

    public void UpdateImpulseStrength(float strength) {
        _impulseStrength = strength;
    }
}