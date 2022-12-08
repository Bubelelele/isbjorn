using UnityEngine;
using MoreMountains.Feedbacks;

public class BreakableIce : MonoBehaviour, IHittable {
    
    [SerializeField] private GameObject destroyableIce;
    [SerializeField] private GameObject breakingIceEffectPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private MMFeedbacks iceSound;

    public void Hit() {
        if (animator != null) {
           
            animator.SetTrigger("Activate");            
        }

        destroyableIce.SetActive(false);
        iceSound?.PlayFeedbacks();
        Instantiate(breakingIceEffectPrefab, transform.position + transform.up, Quaternion.identity);
        IconSystem.instance.TextEnabled(false);
    }
}