using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject deathSplosion;
    public int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        //Finds an object in the scene with a PlayerController script attached to it.
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        //Starting the IEnumerator declared as the RespawnCo Co-Routine.
        StartCoroutine("RespawnCo");
    }

    //The Unity declaration for CoRoutine is called an IEnumerator, which sits parallel to the core loop but can pass its own logic in at its own time.
    public IEnumerator RespawnCo()
    {
        //Once the player dies, we want to cancel all input and activity in the world from that game object.
        thePlayer.gameObject.SetActive(false);

        //Here, we are instantiating the effect to the position we wish to play it. In this case, the deathSplosion is a gameobject which we can tie to the players position and rotation.
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        //As opposed to pausing the whole scene, and causing potential memory leaks or lags in the loop, we can use WaitForSeconds which keeps logic isolated to the player.
        yield return new WaitForSeconds(waitToRespawn);

        //Once the waitToRespawn time has passed, we can throw the player back to their respawn position as specified in CheckPoint Controller, and return their active state to true.
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
    }
}
