using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    //Public variables accessible through the Unity Engine.
    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject deathSplosion, gameOverScreen;
    public int maxHealth, healthCount, coinCount, currentLives, startingLives, bonusLifeThreshold;
    public AudioSource coinSound, levelMusic, gameOverMusic;
    public Text coinText, livesText;
    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartHalf, heartEmpty;
    public bool invincible, respawnCoActive;

    //Private variables unaccessable through the Unity Engine.
    private bool respawning;
    private ResetOnRespawn[] objectsToReset;
    private int coinBonusLifeCount;

    // Start is called before the first frame update
    void Start()
    {
        //Finds an object in the scene with a PlayerController script attached to it.
        thePlayer = FindObjectOfType<PlayerController>();

        //We have not taken any damage yet, so our health would naturally begin as our max health.
        healthCount = maxHealth;

        //This holds the array of objects we want to reset once the player dies. Therefore, we find objects in the world with the ResetOnRespawn script attached.
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        //When the game starts, we want to change the default text to "Coins: " then add our coin value. In this case, it will begin as 0.
        coinText.text = "Coins: " + coinCount;

        if(PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLives = startingLives;
        }

        livesText.text = "Lives x " + currentLives;
    }

    // Update is called once per frame
    void Update()
    {
        /*Since the player can get damaged throughout the game, we want to check for the moment our health goes to 0 or less. We use less than in case something damages us to -1 etc.
        We also want to make sure our Respawning bool is set to false, to avoid circular Respawn logic.*/
        if(healthCount <= 0 && !respawning)
        {
            //We call our Respawn function and CoRoutine once confirmed dead.
            Respawn();
            //Our bool sets to true so we can complete our CoRoutine without accidentally spamming the same logic overtop one another.
            respawning = true;
        }

        if(coinBonusLifeCount >= bonusLifeThreshold)
        {
            currentLives += 1;
            livesText.text = "Lives x " + currentLives;
            coinBonusLifeCount -= bonusLifeThreshold;
        }
    }

    //We create a Respawn function to hold our CoRoutine logic, that way we can publically call the Respawn() to other scripts.
    public void Respawn()
    {
        if(!respawning)
        { 
            currentLives -= 1;
            livesText.text = "Lives x " + currentLives;

            if (currentLives > 0)
            {
                respawning = true;
                //Starting the IEnumerator declared as the RespawnCo Co-Routine.
                StartCoroutine("RespawnCo");
            }
            else
            {
                thePlayer.gameObject.SetActive(false);
                gameOverScreen.SetActive(true);
                levelMusic.Stop(); 
                gameOverMusic.Play();
            }
        }
    }

    //The Unity declaration for CoRoutine is called an IEnumerator, which sits parallel to the core loop but can pass its own logic in at its own time.
    public IEnumerator RespawnCo()
    {
        respawnCoActive = true;
        //Once the player dies, we want to cancel all input and activity in the world from that game object.
        thePlayer.gameObject.SetActive(false);

        //Here, we are instantiating the effect to the position we wish to play it. In this case, the deathSplosion is a gameobject which we can tie to the players position and rotation.
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        //As opposed to pausing the whole scene, and causing potential memory leaks or lags in the loop, we can use WaitForSeconds which keeps logic isolated to the player.
        yield return new WaitForSeconds(waitToRespawn);

        respawnCoActive = false;

        //Since we've respawned, our health returns to max health as it is now treated as a new life.
        healthCount = maxHealth;
        //Our bool is set back to false because we are alive again.
        respawning = false;
        //We want to set our sprites back to indicate full health, so we call our switch case function UpdateHeartMeter for visual indication.
        UpdateHeartMeter();

        //We've reset our progress, so our coin value becomes 0 again.
        coinCount = 0;
        //For visual indication on our UI, we also update our coin value back to 0.
        coinText.text = "Coins: " + coinCount;
        
        coinBonusLifeCount = 0;

        //Once the waitToRespawn time has passed, we can throw the player back to their respawn position as specified in CheckPoint Controller, and return their active state to true.
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;

        coinText.text = "Coins: " + coinCount;

        coinSound.Play();
    } 

    public void HurtPlayer(int damageToTake)
    {
        if(!invincible)
        {
            healthCount -= damageToTake;
            UpdateHeartMeter();
            thePlayer.KnockBack();
            thePlayer.hurtSound.Play();
        }
    }

    public void GiveHealth(int healthToGive)
    {
        healthCount += healthToGive;

        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        coinSound.Play();

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

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;

        coinSound.Play();
    }
}
