using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //Public variables accessible through the Unity Engine.
    public int damageToGive;

    //Private variables unaccessable through the Unity Engine.
    private LevelManager theLevelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //We want to find whatever object in the world has a LevelManager type attached to it.
        theLevelManager = FindObjectOfType<LevelManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Here we check for collision with another object with a 2d Collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Player is the only tag that can be hurt so we want to check for whatever is colliding with us that has the "Player" tag assigned.
        if(other.tag == "Player")
        {
            //Here we call our HurtPlayer function which is initialized in the Level Manager script, but the damage value applied is our damageToGive integer which is set here.
            theLevelManager.HurtPlayer(damageToGive);
        }
    }
}
