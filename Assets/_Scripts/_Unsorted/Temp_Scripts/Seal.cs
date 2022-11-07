using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

public class Seal : MonoBehaviour
{
    public UnityEvent onHit;
    public Transform player;
    public Animator LOD0anim;
    public float pushForce;

    private Rigidbody rb;
    private Animator sealAnim;
    private Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sealAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        dir = transform.position - player.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("yes");
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                rb.isKinematic = false;
                sealAnim.enabled = false;
                LOD0anim.enabled = false;
                rb.AddForce(dir * Time.deltaTime * pushForce);
                Invoke("OnHit", 2f);

            }
        }
    }
    private void OnHit()
    {
        onHit.Invoke();
    }
}
