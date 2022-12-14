using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public GameObject pipMiniGame;

    private bool playerInRange;
    public gameManager pipManager;

    public bool BoatTaskDone = false;

    private bool ending;
    public GameObject endScene;

    private void Awake()
    {
        if(pipManager.miniGameCompleted)
        {
            pipManager.miniGameCompleted = false;
        }

        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && DialogueManager.GetInstance().dialogueComplete)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Debug.Log("Boat button pressed!");
                if(pipManager.miniGameCompleted == false){
                    pipMiniGame.SetActive(true);
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().yOffset = 2.5f;
                }
                else{
                    //continue level
                    if(SceneManager.GetActiveScene().name == "SampleScene")
                    {
                        Debug.Log("SampleScene");
                        StartCoroutine(changeScenes());
                    }
                    if(SceneManager.GetActiveScene().name == "ShipInterior")
                    {   
                        
                    }

                    
                }
                
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    IEnumerator changeScenes()
    {
        yield return new WaitForSeconds(5);
        GameObject.Find("SceneManager").GetComponent<LoadNextScene>().LoadScene(1);
    }
}
