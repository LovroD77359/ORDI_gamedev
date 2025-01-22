using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject Level2Button;
    public GameObject Mistletoe;

    public void Load1()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(2);
        }
        else
        {
            Debug.Log("LevelLoader reference is missing in LoadLevel!");
        }
    }
    public void Load2()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(4);
        }
        else
        {
            Debug.Log("LevelLoader reference is missing in LoadLevel!");
        }
    }
    public void Back() 
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(0);
        }
        else
        {
            Debug.Log("LevelLoader reference is missing in LoadLevel!");
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("LevelsCompleted", 0);
        Level2Button.SetActive(false);
        Mistletoe.SetActive(false);
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
    }

    private void Start()
    {
        // Directly access the singleton instance of LevelLoader
        if (LevelLoader.instance == null)
        {
            Debug.LogError("LevelLoader instance not found!");
        }

        // Get the number of levels completed from PlayerPrefs (default to 0 if no data exists)
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
        Level2Button.SetActive(false);
        Mistletoe.SetActive(false);

        // If Level 1 is completed, enable Level 2 button
        if (levelsCompleted >= 1 && Level2Button != null)
        {
            Level2Button.SetActive(true); // Activate Level 2 access button
        }

        // If both levels are completed, show the special object in LoadLevel
        if (levelsCompleted == 2 && Mistletoe != null)
        {
            Mistletoe.SetActive(true); // Activate the special object
        }
    }


}
