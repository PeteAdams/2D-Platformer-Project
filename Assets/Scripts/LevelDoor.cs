using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{

    public string levelToLoad;
    public bool unlocked;
    public Sprite doorBottomOpen, doorTopOpen, doorBottomClosed, doorTopClosed;
    public SpriteRenderer doorTop, doorBottom;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level1", 1);

        if(PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }

        if(unlocked)
        {
            doorTop.sprite = doorTopOpen;
            doorBottom.sprite = doorBottomOpen;
        }
        else
        {
            doorTop.sprite = doorTopClosed;
            doorBottom.sprite = doorBottomClosed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetButton("Jump") && unlocked)
            {
                SceneManager.LoadScene(levelToLoad);  
            }
        }
    }
}
