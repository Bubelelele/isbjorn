using UnityEngine;

public class BreakableIce : MonoBehaviour, IHittable {
    
    [SerializeField] private GameObject destroyableIce;
    [SerializeField] private GameObject breakingIceEffectPrefab;
    [SerializeField] private Animator animator;

    public void Hit() {
        if (animator != null) {
            animator.SetTrigger("Activate");
        }

        destroyableIce.SetActive(false);
        Instantiate(breakingIceEffectPrefab, transform.position + transform.up, Quaternion.identity);
        IconSystem.instance.TextEnabled(false);
    }
}