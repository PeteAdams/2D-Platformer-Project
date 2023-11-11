using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    //Public variables accessible through the Unity Engine.
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*For temporary objects such as partile effects or objects that have lifetime and are not meant to remain in world, we want to initialize a lifetime.
        The reason we subtract the lifetime by Time.deltaTime is because each machine has a different frame rate, and would feel most natural if we use that as a subtracting value.
        For instance, if a frame completes in 0.0013 seconds, it would look like 1.1 - 0.0013 until it is below 0.*/
        lifeTime = lifeTime - Time.deltaTime;

        //Checking for once deltaTime ticks down enough to get lifetime to 0.
        if(lifeTime <= 0 )
        {
            //Destroying the object, because we have no use for it now or in the future.
            Destroy(gameObject);
        }
    }
}
