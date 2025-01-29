using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Strip : MonoBehaviour
{
    
    private int pageNumber = 0;
    private int rawImageIndex = 0;
    private GameObject[] pages;
    private RawImage[][] rawImagesArray;

    // Start is called before the first frame update
    void Start()
    {
        // Dynamically find all child GameObjects starting with "Page"
        List<GameObject> foundPages = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.StartsWith("Page"))
            {
                foundPages.Add(child.gameObject);
            }
        }

        // Sort pages by name to ensure they are ordered correctly (Page1, Page2, etc.)
        foundPages.Sort((a, b) => a.name.CompareTo(b.name));
        pages = foundPages.ToArray();

        rawImagesArray = new RawImage[pages.Length][];
        for (int i = 0; i < pages.Length; i++)
        {
            rawImagesArray[i] = pages[i].GetComponentsInChildren<RawImage>();
            for (int j = 0; j < rawImagesArray[i].Length; j++) 
            {
                rawImagesArray[i][j].gameObject.SetActive(false);    
            }
            if (i > 0)
            {
                pages[i].SetActive(false);
            }
        }

        ActivateRawImage(pageNumber, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            if (rawImageIndex < rawImagesArray[pageNumber].Length - 1)
            {
                rawImageIndex++;                                        // Move to the next RawImage
                ActivateRawImage(pageNumber, rawImageIndex);            // Show the next RawImage
            }
            else if (pageNumber < pages.Length - 1)                     // Move to the next page if possible
            {
                rawImageIndex = 0;                                      // Reset RawImage index for the new page
                pageNumber++;                                           // Move to the next page
                flip();                                                 // Show the new page
                ActivateRawImage(pageNumber, rawImageIndex);            // Show the first RawImage on the new page
                Debug.Log("Sada si na " + pageNumber  + ". stranici");
            }
            else if (pageNumber == pages.Length - 1) 
            {
                Scene nextScene = SceneManager.GetActiveScene();
                SceneManager.LoadSceneAsync(nextScene.buildIndex + 1);
            }
        }
        
    }

    private void flip() 
    {
        for (int i = 0; i < pages.Length; i++) 
        {
            if (i == pageNumber)
            {
                pages[i].gameObject.SetActive(true);
            }
            else 
            {
                pages[i].gameObject.SetActive(false);
            }   
        }   
    }

    private void ActivateRawImage(int page, int rawImage)
    {
        // Activate the specified RawImage on the page
        if (rawImagesArray[page].Length > rawImage)
        {
            rawImagesArray[page][rawImage].gameObject.SetActive(true);
        }
    }
}
