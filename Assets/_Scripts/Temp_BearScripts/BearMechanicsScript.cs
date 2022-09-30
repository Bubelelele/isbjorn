using UnityEngine;

public class BearMechanicsScript : MonoBehaviour
{
    public Animator bearAnim;

    private void Update()
    {
        //Sniffing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bearAnim.SetTrigger("Sniff");
        }
        //Roaring
        if (Input.GetKeyDown(KeyCode.E))
        {
            bearAnim.SetTrigger("Roar");
        }
        //Slashing
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bearAnim.SetTrigger("Slash");
        }
    }
}
