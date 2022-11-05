using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishInMouth : MonoBehaviour
{
    public float foodAmount;

    public PlayerHunger playerHunger;
    public bool hasEaten;


    // Start is called before the first frame update
    void Start()
    {
        playerHunger = GameObject.Find("Player").GetComponent<PlayerHunger>();
        hasEaten = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && !hasEaten)
        {
            //Debug.Log("Eat");
            Eat();
        }

    }


    public void Eat()
    {
        hasEaten = false; 
        playerHunger.AddFood(foodAmount);
        gameObject.SetActive(false);
    }


}
