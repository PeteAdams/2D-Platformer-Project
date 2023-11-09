using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject deathSplosion;
    public int coinCount;
    public int maxHealth, healthCount;
    public Text coinText;

    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartHalf, heartEmpty;

    private bool respawning;

    // Start is called before the first frame update
    void Start()
    {
        //Finds an object in the scene with a PlayerController script attached to it.
        thePlayer = FindObjectOfType<PlayerController>();

        coinText.text = "Coins: " + coinCount;

        healthCount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
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

        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        //Once the waitToRespawn time has passed, we can throw the player back to their respawn position as specified in CheckPoint Controller, and return their active state to true.
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;

        coinText.text = "Coins: " + coinCount;
    }

    public void HurtPlayer(int damageToTake)
    {
        healthCount -= damageToTake;
        UpdateHeartMeter();
    }

    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }
}
