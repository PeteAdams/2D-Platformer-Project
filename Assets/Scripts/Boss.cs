using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public bool bossActive, bossRight, takeDamage, waitingForRespawn;
    public float timeBetweenDrops, waitForPlatforms;
    public int startingHealth;
    public Transform leftPoint, rightPoint, dropSawSpawnPoint;
    public GameObject dropSaw, theBoss, rightPlatforms, leftPlatforms, levelExit;

    private float dropCount, platformCount, timeBetweenDropsStore;
    private int currentHealth;
    private CameraController theCamera;
    private LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();

        dropCount = timeBetweenDrops;
        timeBetweenDropsStore = timeBetweenDrops;
        platformCount = waitForPlatforms;
        currentHealth = startingHealth;

        theBoss.transform.position = rightPoint.position;
        bossRight = true;
    }
     
    // Update is called once per frame
    void Update()
    {
        if (theLevelManager.respawnCoActive)
        {
            bossActive = false;
            waitingForRespawn = true;
        }

        if (waitingForRespawn && !theLevelManager.respawnCoActive)
        {
            theBoss.SetActive(false);
            leftPlatforms.SetActive(false);
            rightPlatforms.SetActive(false);

            timeBetweenDrops = timeBetweenDropsStore;

            platformCount = waitForPlatforms;
            dropCount = timeBetweenDrops;

            theBoss.transform.position = rightPoint.position;
            bossRight = true;
            currentHealth = startingHealth;

            theCamera.followTarget = true;

            waitingForRespawn = false;
        }

        if (bossActive)
        {
            theCamera.followTarget = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothing * Time.deltaTime);

            theBoss.SetActive(true);
            if(dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            }
            else
            {
                dropSawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
                dropCount = timeBetweenDrops;
            }

            if(bossRight)
            {
                if(platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    rightPlatforms.SetActive(true);
                }
            }
            else
            {
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    leftPlatforms.SetActive(true);
                }
            }

            if(takeDamage)
            {
                currentHealth -= 1;

                if(currentHealth <= 0)
                {
                    levelExit.SetActive(true);
                    theCamera.followTarget = true;
                    gameObject.SetActive(false);
                }

                if(bossRight)
                {
                    theBoss.transform.position = leftPoint.position;
                }
                else
                {
                    theBoss.transform.position = rightPoint.position;
                }

                bossRight = !bossRight;

                rightPlatforms.SetActive(false);
                leftPlatforms.SetActive(false);

                platformCount = waitForPlatforms;

                timeBetweenDrops = timeBetweenDrops / 2f;

                takeDamage = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            bossActive = true;
        }
    }
}
