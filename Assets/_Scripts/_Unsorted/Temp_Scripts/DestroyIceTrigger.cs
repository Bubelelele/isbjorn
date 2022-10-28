using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyIceTrigger : MonoBehaviour
{
    public GameObject destroyableIce;
    public GameObject breakingIceEffectPrefab;
    
    private bool canDestroy;
    private bool isDestroyed;
    private Animator blockAnim;

    private void Start()
    {
        if(GetComponent<Animator>() != null)
        blockAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(canDestroy && Mouse.current.leftButton.wasPressedThisFrame && !isDestroyed)
        {
            if(blockAnim != null)
            {
                blockAnim.SetTrigger("Activate");
            }
            destroyableIce.SetActive(false);
            GameObject effect = Instantiate(breakingIceEffectPrefab, transform.position + transform.up, Quaternion.identity);
            Destroy(effect, 1f);
            isDestroyed = true;
                
        }
    }
    public void CanDestroy()
    {
        canDestroy = true;
    }
    public void CannotDestroy()
    {
        canDestroy = false;
    }
}
