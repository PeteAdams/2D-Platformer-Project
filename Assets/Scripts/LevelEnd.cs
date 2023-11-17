using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public string levelToLoad, levelToUnlock;
    public float waitToMove, waitToLoad;
    public Sprite flagOpen;
    public GameObject checkpointSplosion;

    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;
    private bool movePlayer;
    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();

        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movePlayer)
        {
            thePlayer.myRigidBody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidBody.velocity.y, 0f);
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theSpriteRenderer.sprite = flagOpen;
            StartCoroutine("LevelEndCo");
        }
    }

    public IEnumerator LevelEndCo()
    {
        Instantiate(checkpointSplosion, gameObject.transform.position, gameObject.transform.rotation);

        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.invincible = true;

        theLevelManager.levelMusic.Stop();
        theLevelManager.gameOverMusic.Play();

        thePlayer.myRigidBody.velocity = Vector3.zero;

        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);

        PlayerPrefs.SetInt(levelToUnlock, 1);

        yield return new WaitForSeconds(waitToMove);

        movePlayer = true;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(levelToLoad);
    }
}
