using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;
    public GameObject Mistletoe;

    private void Start()
    {
        if (!IsLevelLoaderAvailable())
        {
            Level2Button.SetActive(false); // Disable buttons if LevelLoader is missing
            Level3Button.SetActive(false);
            Level4Button.SetActive(false);
            Mistletoe.SetActive(false);
            return;
        }

        // Get and log the levels completed from PlayerPrefs
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Debug.Log("Levels Completed: " + levelsCompleted);

        UpdateUI();
    }

    private bool IsLevelLoaderAvailable()
    {
        if (LevelLoader.instance == null)
        {
            Debug.LogError("LevelLoader reference is missing in LoadLevel!");
            return false;
        }
        return true;
    }

    public void Load(int levelIndex)
    {
        if (IsLevelLoaderAvailable())
        {
            LevelLoader.instance.LoadNewLevel(levelIndex);
        }
    }

    public void Back()
    {
        if (IsLevelLoaderAvailable())
        {
            LevelLoader.instance.LoadNewLevel(0);
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("LevelsCompleted", 0);
        PlayerPrefs.Save();
        UpdateUI(); // Update the UI state after reset
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
    }

    private void UpdateUI()
    {
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Level2Button.SetActive(levelsCompleted >= 1); // Show Level 2 button if Level 1 is completed
        Level3Button.SetActive(levelsCompleted >= 2);
        Level4Button.SetActive(levelsCompleted >= 3);
        Mistletoe.SetActive(levelsCompleted == 4);   // Show Mistletoe if both levels are completed
    }
}
