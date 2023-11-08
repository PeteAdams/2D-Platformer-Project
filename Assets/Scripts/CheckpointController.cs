using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public bool checkpointActive;
    public Sprite flagClosed, flagOpen;

    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //Once we confirm the player has collided with the flagpole, we change the sprite to the open flag.
            theSpriteRenderer.sprite = flagOpen;
            //This becomes our new checkpoint, thus making the respawn point this transform.
            checkpointActive = true;
        }
    }
}
