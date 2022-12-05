using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour {
    [Header("Player Hunger")] public float maxHunger = 100;
    public float hunger;
    public float hungerOverTime;
    public Slider hungerSlider;
    public MMFeedbacks lowFoodFeedback;
    public float lowFoodPoint = 20;
    public Animator playerAnim;

    private PlayerStateMachine playerStateMachine;
    private CP_Respawn cp_Respawn;
    private FadeToBlack fadeToBlack;
    private bool hasHappened = false;

    private void Awake()
    {
        cp_Respawn = this.gameObject.GetComponent<CP_Respawn>();
        fadeToBlack = FindObjectOfType<FadeToBlack>();
        playerStateMachine= GetComponent<PlayerStateMachine>();
    }

    // Start is called before the first frame update
    void Start() {
        hungerSlider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update() {
        hunger = hunger - hungerOverTime * Time.deltaTime;

        if (hunger <= 0) {
            hunger = 0;
            if (!hasHappened)
            {
                StartCoroutine("Death");
                hasHappened = true; 

            }
        }

        if (hunger > 100) {
            hunger = 100;
        }

        UpdateSliders();
        UpdateFeedback();
    }

    public void AddFood(float Amount) {
        this.hunger += Amount;
    }

    public void RemoveFood(float Amount) {
        this.hunger -= Amount;
    }


    void UpdateSliders() {
        if (hungerSlider != null)
            hungerSlider.value = hunger / maxHunger;
    }

    private void UpdateFeedback() {
        if (lowFoodFeedback.IsPlaying) {
            if (hunger >= lowFoodPoint) lowFoodFeedback.StopFeedbacks();
        } else if (hunger < lowFoodPoint) {
            lowFoodFeedback.PlayFeedbacks();
        }
    }
   

    private IEnumerator Death()
    {
        playerAnim.SetTrigger("Die");
        playerStateMachine.enabled = false;
        yield return new WaitForSeconds(2.5f);
        fadeToBlack?.StartFade();
        yield return new WaitForSeconds(3);
        playerStateMachine.enabled = true;
        cp_Respawn.RespawnPlayer();
        AddFood(50);
        fadeToBlack?.StopFade();
        hasHappened = false; 
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Icicle"))
		{
            hunger--;
		}
	}
}
