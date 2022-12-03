using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour
{
    [Header("Player Hunger")]

    public float maxHunger = 100;
    public float hunger;
    public float hungerOverTime;
    public Slider hungerSlider;

    // Start is called before the first frame update
    void Start()
    {
        hungerSlider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hunger = hunger - hungerOverTime * Time.deltaTime;

        if (hunger <= 0)
        {
            hunger = 0;
        }
        if (hunger > 100)
        {
            hunger = 100; 
        }

        UpdateSliders();
    }

    public void AddFood(float Amount)
    {
        this.hunger += Amount; 
    }

    public void RemoveFood(float Amount)
    {
        this.hunger -= Amount;
    }


    void UpdateSliders()
    {
        hungerSlider.value = hunger / maxHunger; 
    }
}
