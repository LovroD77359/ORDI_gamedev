using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            if (LevelLoader.instance != null)
            {
                LevelLoader.instance.LoadNewLevel(0); // Load Title Page (Scene Index 0) with transition
            }
            else
            {
                Debug.LogWarning("LevelLoader instance not found! Loading scene directly.");
                SceneManager.LoadSceneAsync(0);
            }
        } 
    }
}
