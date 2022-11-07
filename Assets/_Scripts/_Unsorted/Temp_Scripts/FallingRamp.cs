using UnityEngine;

public class FallingRamp : MonoBehaviour
{
    public Animator rampAnim;
    public GameObject ice1, ice2, ice3;
    public GameObject pendelIce;
    public GameObject breakingIceEffectPrefab;
    public Animator pendelAnim;

    private void Update()
    {
        if(!ice1.activeSelf && !ice2.activeSelf && !ice3.activeSelf)
        {
            // Debug.Log("yes");
            RampDown();
        }
    }

    private void RampDown()
    {
        rampAnim.SetTrigger("RampDown");
    }
    private void DestroyPendelIce()
    {
        pendelIce.SetActive(false);
        GameObject effect = Instantiate(breakingIceEffectPrefab, pendelIce.transform.position + pendelIce.transform.up, Quaternion.identity);
        effect.transform.localScale = new Vector3(effect.transform.localScale.x * 5, effect.transform.localScale.y * 5, effect.transform.localScale.z * 5);
        Destroy(effect, 1f);
        Destroy(pendelIce, 1f);
        pendelAnim.SetTrigger("StartPendel");
    }
}
