using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour
{

    //Public variables accessible through the Unity Engine.
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;

    //Private variables unaccessable through the Unity Engine.
    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //Here we get the rigidbody that is attached to the object posessing a GreenWiggleController script.
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*We just want to start the function by moving our Green Wiggle in its correct direction. Since we have a bool called movingRight, we want to start by moving right.
         Therefore, we only stop moving right if our x position surpasses our rightPoint, which is our set limit in the engine.*/
        if(movingRight && transform.position.x > rightPoint.position.x)
        {
            //Set the bool to false to indicate we are no longer moving right.
            movingRight = false;
        }
        //We inverse the logic of the above if statement, and apply the same concept if the position of our Green Wiggle surpasses our leftPoint.
        if(!movingRight && transform.position.x < leftPoint.position.x)
        {
            //Set the bool back to true becasue we are now moving right again.
            movingRight = true;
        }

        //Now that we have the left and right logic completed, we now want to indicate how fast our Green Wiggle is moving. If we are moving right, we add a positive value to X.
        if(movingRight)
        {
            //Our movespeed value is set in the Unity Engine. We use the same principle as we did with player movement, but without the input.
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
        }
        //There are no other cases with bools other than true and false, so we can just use else to indicate false.
        else
        {
            //Since we are now moving left, we just set the movespeed to negative on the X axis.
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }
}
