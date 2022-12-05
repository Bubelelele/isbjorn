using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishInMouth : MonoBehaviour
{
    public float foodAmount;

    public PlayerHunger playerHunger;
    public bool hasEaten, hasFish;
    public GameObject fishPrefab;
    public GameObject spawnPosition;

    private GameObject myGameObject;


    // Start is called before the first frame update
    void Start()
    {
        playerHunger = GameObject.Find("Player").GetComponent<PlayerHunger>();
        hasEaten = false;
        hasFish = true;
        myGameObject = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (myGameObject.activeInHierarchy)
        {
            hasFish = true;
            IconSystem.instance.FishInMouth();

        }

        if (Keyboard.current.fKey.wasPressedThisFrame && hasFish)
        {
            //Debug.Log("Eat");
            Eat();
            
        }

        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            DropFish();
        }

        
    }
    
    public void Eat()
    {
        hasEaten = true;
        hasFish = false; 
        playerHunger.AddFood(foodAmount);
       //hasEaten = false; 
        gameObject.SetActive(false);
        //IconSystem.instance.FishInMouth();
        IconSystem.instance.TextEnabled(false);

    }

    private void DropFish()
    {
        hasFish = false;

        //Debug.Log("SpawnFish");
        GameObject fish = Instantiate(fishPrefab, spawnPosition.transform.position, Quaternion.identity) as GameObject;
        fish.GetComponent<Food>().foodIsDropped = true;
        gameObject.SetActive(false);
        IconSystem.instance.TextEnabled(false);
    }


}
