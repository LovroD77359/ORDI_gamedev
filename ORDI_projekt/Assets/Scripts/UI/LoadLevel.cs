using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject Level2button;
    public GameObject Imela;

    public void Load1()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Load2()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Back() 
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("LevelsCompleted", 0);
        Level2button.SetActive(false);
        Imela.SetActive(false);
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
    }

    private void Start()
    {
        // Get the number of levels completed from PlayerPrefs (default to 0 if no data exists)
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Debug.Log("PlayerPrefs Saved (" + PlayerPrefs.GetInt("LevelsCompleted", 0) + ")");
        Level2button.SetActive(false);
        Imela.SetActive(false);

        // If Level 1 is completed, enable Level 2 button
        if (levelsCompleted >= 1 && Level2button != null)
        {
            Level2button.SetActive(true); // Activate Level 2 access button
        }

        // If both levels are completed, show the special object in LoadLevel
        if (levelsCompleted == 2 && Imela != null)
        {
            Imela.SetActive(true); // Activate the special object
        }
    }


}
