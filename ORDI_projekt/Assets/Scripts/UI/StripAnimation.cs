using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StripAnimation : MonoBehaviour
{
    [HideInInspector] public int panelNumber = 1;

    private Animator animator;
    private int panelCount = 0;
    private bool loadingNextScene = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        RawImage[] rawImagesArray;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.StartsWith("Page"))
            {
                Debug.Log(child.gameObject.name);
                rawImagesArray = child.gameObject.GetComponentsInChildren<RawImage>();
                Debug.Log(rawImagesArray.ToString());
                panelCount += rawImagesArray.Length;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelLoader.instance != null)
            {
                LevelLoader.instance.LoadNewLevel(1);
            }
            else
            {
                Debug.LogError("LevelLoader instance not found! Loading scene directly.");
                SceneManager.LoadScene("Load level");
            }
        }
        else if (Input.anyKeyDown) 
        {
            panelNumber++;
            animator.SetInteger("panelNumber", panelNumber);
        }
        if (panelNumber > panelCount && !loadingNextScene)
        {
            loadingNextScene = true;
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            if (LevelLoader.instance != null)
            {
                LevelLoader.instance.LoadNewLevel(currentLevelIndex + 1);
            }
            else
            {
                Debug.LogError("LevelLoader instance not found! Loading scene directly.");
                SceneManager.LoadSceneAsync(currentLevelIndex + 1);
            }
        }
    }
}
