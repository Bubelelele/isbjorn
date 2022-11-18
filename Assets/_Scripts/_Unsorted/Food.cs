using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Food : MonoBehaviour
{

    public bool foodIsDropped;
    public float howMuchFood;

    public PlayerHunger playerHunger;
    private bool foodIsEaten;

    // Start is called before the first frame update
    void Awake()
    {
        foodIsDropped = false;

        playerHunger = GameObject.Find("Player").GetComponent<PlayerHunger>();
        foodIsEaten = false;
    }


    //// Update is called once per frame
    //void Update()
    //{
    //    if (Keyboard.current.gKey.wasPressedThisFrame && !foodIsEaten)
    //    {
    //        EatFood();
    //    }
    //}


    public void EatFood()
    {
        playerHunger.AddFood(howMuchFood);
        IconSystem.instance.TextEnabled(false);
        foodIsEaten = true;
    }
    public void FoodIsDropped()
    {
        IconSystem.instance.TextEnabled(false);
        foodIsDropped = true;
    } 
}
