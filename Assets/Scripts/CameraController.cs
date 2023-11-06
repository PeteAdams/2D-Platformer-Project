using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Public variables accessible through the Unity Engine.
    public float followAhead, smoothing;
    public GameObject target;

    //Private variables unaccessable through the Unity Engine.
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Setting the camera transform vector to only follow the players X axis, locking the Y and Z to its own axis so the elevation doesn't change.
        targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

        //Offsetting the camera by a float value on the X axis, this makes the game feel a bit better for visibility and allows the player to see ahead.
        if(target.transform.localScale.x > 0f)
        {
            //Adding arbitrary value to X.
            targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
        }
        else
        {
            //Subtracting arbitrary value to X due to player walking left.
            targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
        }

        //Using the Lerp function to gradually move the camera left and right based on the direction of the player. Smoothing value is the speed which it flows back and forth.
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
