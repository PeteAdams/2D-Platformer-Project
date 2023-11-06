using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawn;
    public PlayerController thePlayer;

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
        StartCoroutine("RespawnCo");
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
    }
}
