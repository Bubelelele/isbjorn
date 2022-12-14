using UnityEngine;
using UnityEngine.Events;

public class CallUnityEvent : MonoBehaviour {
    [SerializeField] private UnityEvent[] onCall;

    public void Call(int i) {
        onCall[i].Invoke();
    }
}