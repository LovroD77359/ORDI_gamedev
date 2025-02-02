using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGate : MonoBehaviour
{
    private int playerContact = 0;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerContact++;
        }

        if (playerContact == 2)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);

            // Only update PlayerPrefs if it's a playable level (not a Strip scene)
            if (IsPlayableLevel(currentLevel) && levelsCompleted < GetLevelNumber(currentLevel))
            {
                PlayerPrefs.SetInt("LevelsCompleted", GetLevelNumber(currentLevel));
                PlayerPrefs.Save();
                Debug.Log("Level " + GetLevelNumber(currentLevel) + " Completed!");
                Debug.Log("PlayerPrefs Saved: " + PlayerPrefs.GetInt("LevelsCompleted", 0));
            }

            LoadNextScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Decrease the count when a player leaves the collision zone
        if (other.gameObject.CompareTag("Player"))
        {
            playerContact--;
        }
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(nextSceneIndex); // Use LevelLoader for animated transitions
        }
        else
        {
            Debug.LogWarning("LevelLoader instance not found! Loading scene directly.");
            SceneManager.LoadSceneAsync(nextSceneIndex); // Fallback if LevelLoader is missing
        }
    }

    private bool IsPlayableLevel(int sceneIndex)
    {
        // Only count actual levels (not Strip scenes)
        return sceneIndex == 3 || sceneIndex == 4 || sceneIndex == 6 || sceneIndex == 7;
    }

    private int GetLevelNumber(int sceneIndex)
    {
        // Maps sceneIndex to Level Number for PlayerPrefs
        switch (sceneIndex)
        {
            case 3: return 1;  // Level 1
            case 4: return 2;  // Level 2
            case 6: return 3;  // Level 3
            case 7: return 4;  // Level 4
            default: return 0; // Not a valid playable level
        }
    }
}
