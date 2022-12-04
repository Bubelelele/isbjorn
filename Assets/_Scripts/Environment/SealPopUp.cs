using UnityEngine;

public class SealPopUp : MonoBehaviour, IHittable
{

    public float distanceUntilShow = 20f;
    public float maxWaitTime = 10f;
    public int foodToAdd = 30;
    public GameObject PoofEffect;

    private Animator sealAnim;
    private PlayerHunger playerHunger;
    private string methodToInvoke;
    private float distance;
    private bool showing;
    private bool invoked;


    private void Start()
    {
        sealAnim = GetComponent<Animator>();
        playerHunger = PlayerStateMachine.staticPlayerTransform.gameObject.GetComponent<PlayerHunger>();

    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, PlayerStateMachine.staticPlayerTransform.position);

        if(distance < distanceUntilShow)
        {
            SealBehavior();
        }
        else
        {
            CancelInvoke();
            HideSeal();
        }
    }

    private void SealBehavior()
    {
        if (!showing)
        {
            methodToInvoke = "ShowSeal";
        }
        else
        {
            methodToInvoke = "HideSeal";
        }
        if (!invoked)
        {
            Invoke(methodToInvoke, Random.Range(1, maxWaitTime));
            invoked = true;
        }

        sealAnim.SetBool("Show", showing);
    }
    public void ShowSeal()
    {
        showing = true;
        invoked = false;
    }
    public void HideSeal()
    {
        showing = false;
        invoked = false;
    }

    public void Hit()
    {
        if (showing)
        {
            playerHunger.AddFood(foodToAdd);
            sealAnim.SetTrigger("Die");
            Destroy(gameObject, 4);
        }
    }
    private void OnDestroy()
    {
        GameObject effect = Instantiate(PoofEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Destroy(effect, 1f);
    }
}
