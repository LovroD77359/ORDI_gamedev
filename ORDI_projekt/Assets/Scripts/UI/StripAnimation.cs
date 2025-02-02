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
            SceneManager.LoadSceneAsync(1);
        }
        else if (Input.anyKeyDown) 
        {
            panelNumber++;
            animator.SetInteger("panelNumber", panelNumber);
        }
        if (panelNumber > panelCount)
        {
            Scene nextScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(nextScene.buildIndex + 1);
        }
    }
}
