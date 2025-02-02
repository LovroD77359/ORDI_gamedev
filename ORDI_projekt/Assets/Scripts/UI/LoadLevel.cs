using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject Level2Button;
    public GameObject Dots12;
    public GameObject Level3Button;
    public GameObject Dots23;
    public GameObject Level4Button;
    public GameObject Dots34;
    public RawImage Background;
    public Texture LevelSelectBg;
    public Texture LevelSelectBgFinished;

    private void Start()
    {
        if (!IsLevelLoaderAvailable())
        {
            Level2Button.SetActive(false); // Disable buttons if LevelLoader is missing
            Dots12.SetActive(false);
            Level3Button.SetActive(false);
            Dots23.SetActive(false);
            Level4Button.SetActive(false);
            Dots34.SetActive(false);
            
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

    public void SetLevelsComplete(int number)
    {
        PlayerPrefs.SetInt("LevelsCompleted", number);
        PlayerPrefs.Save();
        UpdateUI(); // Update the UI state after reset
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", number) + ")");
    }

    private void UpdateUI()
    {
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Level2Button.SetActive(levelsCompleted >= 1); // Show Level 2 button if Level 1 is completed etc
        Dots12.SetActive(levelsCompleted >= 1);
        Level3Button.SetActive(levelsCompleted >= 2);
        Dots23.SetActive(levelsCompleted >= 2);
        Level4Button.SetActive(levelsCompleted >= 3);
        Dots34.SetActive(levelsCompleted >= 3);

        if (levelsCompleted == 4)
        {
            Background.texture = LevelSelectBgFinished;
        }
        else
        {
            Background.texture = LevelSelectBg;
        }
    }
}
