using _Scripts.Input;
using UnityEngine;

public class DestroyIceTrigger : MonoBehaviour
{
    public PlayerInput input;
    public GameObject destroyableIce;
    public GameObject breakingIceEffectPrefab;
    
    private bool canDestroy;
    private bool isDestroyed;
    private Animator blockAnim;

    private void Start()
    {
        blockAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(canDestroy && input.Slashing && !isDestroyed)
        {
            blockAnim.SetTrigger("Activate");
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
