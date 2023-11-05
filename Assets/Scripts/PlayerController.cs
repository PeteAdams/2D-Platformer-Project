using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //These public values will be accessible through the inspector in the Unity Engine.
    public float moveSpeed, jumpSpeed, groundCheckRadius;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    //These private values will not be accessible through the inspector in the Unity Engine.
    private Rigidbody2D myRigidBody;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Automatically finds the attached component to the player
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Small circle initialized beneath the player which checks for collision. Position + radius colliding with the "Ground" tag as identified in engine public variable.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Utilizes input values as initialized by default in engine, Horizontal is recognized by A and D keys, therefore we can use a float value to indicate speed.
        if(Input.GetAxisRaw("Horizontal") > 0f)
        {
            //If the speed is greater than 0, we want to move right. Rather than keeping Y value null, we want to keep the original gravity value so it will not interfere.
            myRigidBody.velocity = new Vector3(moveSpeed,myRigidBody.velocity.y, 0f);
            //Since the game is 2D, we want to flip the sprite based on the direction he's looking. Since default animation is looking to the right, we can keep the scale as is.
            transform.localScale = new Vector3(1f, 1f, 1f);
        }    
        else if(Input.GetAxisRaw("Horizontal") < 0f)
        {
            /*If the speed is less than 0, we want to move to the left. Since unity recognizes the A key as a negative value, our move speed should be inverted by putting a - in
            front of the moveSpeed value.*/
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
            //Since we're moving left, we can flip the scale of the sprite's X value to a -1. This will change the animation direction and provide player feedback of direction.
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            //The only remaining condition could mean Horizontal = 0, which allows us to zero out the movement speed entirely.
            myRigidBody.velocity = new Vector3(0, myRigidBody.velocity.y, 0);
        }

        //Using the Unity Engine "Jump" input system, we're able to assign jump to the spacebar immediately. We also want isGrounded to be true, otherwise the player can jump mid-air.
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //We assign a public value for jumpSpeed to indicate the velocity the player will jump on the Y axis. Everything else can keep the same velocity as previous.
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
        }

        /*We assign our animation parameters to our code values to help with our blend poses. Mathf.Abs takes the absolute value of our speed. Therefore, if we're moving left,
        speed will be a positive number and will not confuse our animation parameters.*/
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        //Here we simply assign our Grounded parameter to equal our Isgrounded variable in code. This will toggle the jump animation based on a boolean.
        myAnim.SetBool("Grounded", isGrounded);
    }

}
