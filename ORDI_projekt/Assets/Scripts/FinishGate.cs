using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGate : MonoBehaviour
{
    
    private int playerContact = 0;

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact++;
        }

        if (playerContact == 2)
        {
            if (PlayerPrefs.GetInt("LevelsCompleted", 0) != 2)
            {
                int currentLevel = SceneManager.GetActiveScene().buildIndex;

                if (currentLevel == 3) // Assuming Level 1 is scene index 2
                {

                    PlayerPrefs.SetInt("LevelsCompleted", 1); // Level 1 completed
                    Debug.Log("Level 1 Completed");
                }
                else if (currentLevel == 4) // Assuming Level 2 is scene index 4
                {
                    PlayerPrefs.SetInt("LevelsCompleted", 2); // Level 2 completed
                    Debug.Log("Level 2 Completed");
                }


                PlayerPrefs.Save(); // Save the progress
                Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
            }

            LoadNextScene();

            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // Decrease the count when a player leaves the collision zone
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact--;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
