using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    //Public variables accessible through the Unity Engine.
    public bool checkpointActive;
    public Sprite flagClosed, flagOpen;

    //Private variables unaccessable through the Unity Engine.
    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //Here we initialize the sprite renderer that is attached to the component of whatever the script is attached to. In this case, our checkpoint.
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Here we check if another collider makes contact with our flagpole.
    void OnTriggerEnter2D(Collider2D other)
    {
        //We want to check if the colliding entity is a player, because players are the only tags that can activate checkpoints.
        if(other.tag == "Player")
        {
            //Once we confirm the player has collided with the flagpole, we change the sprite to the open flag. This is set in our public field in-engine.
            theSpriteRenderer.sprite = flagOpen;
            //This becomes our new checkpoint, thus making the respawn point this transform.
            checkpointActive = true;
        }
    }
}
