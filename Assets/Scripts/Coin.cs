using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Public variables accessible through the Unity Engine.
    public int coinValue;

    //Private variables unaccessable through the Unity Engine.
    private LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        //Here we check for objects in the world that have a LevelManager script attached to them. We ignore everything else that doesn't have the script.
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Here we check if our coin is colliding with another collider object.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //We check if the colliding object has the "Player" tag, because the player is the only entity that can pick up coins.
        if(other.tag == "Player")
        {
            //If the player successfully collides with the coin, we want to call the levelmanager Addcoins function to add to the amount of coins picked up.
            theLevelManager.AddCoins(coinValue);
            //Instead of destroying the coin, we want to set inactive so they can reset if player dies.
            gameObject.SetActive(false);
        }
    }
}
