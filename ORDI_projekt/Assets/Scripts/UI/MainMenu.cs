using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // Directly access the singleton instance of LevelLoader
        if (LevelLoader.instance == null)
        {
            Debug.LogError("LevelLoader instance not found!");
        }
    }

    public void LoadLevelSelect()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(1);
        }
        else
        {
            Debug.Log("LevelLoader reference is missing in MainMenu!");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
