using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderController : MonoBehaviour
{

    public float moveSpeed;

    private bool canMove;
    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }

    // As soon as any object appears or disappears in the camera's view, it will run the code in OnBecame functions.
    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Added an empty game object with the KillPlane tag to fire off when the player falls to their death.
        if (other.tag == "KillPlane")
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        canMove = false;
    }
}
