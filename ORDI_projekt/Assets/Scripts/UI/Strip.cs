using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Strip : MonoBehaviour
{
    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;
    public GameObject Page4;
    public GameObject Page5;

    private int pageNumber = 0;
    private GameObject[] pages;
    private RawImage[][] rawImagesArray;

    // Start is called before the first frame update
    void Start()
    {
        Page1.gameObject.SetActive(true);
        Page2.gameObject.SetActive(false);
        Page3.gameObject.SetActive(false);
        Page4.gameObject.SetActive(false);
        Page5.gameObject.SetActive(false);
 
        pages = new GameObject[] { Page1, Page2, Page3, Page4, Page5};

        rawImagesArray = new RawImage[pages.Length][];
        for (int i = 0; i < pages.Length; i++)
        {
            rawImagesArray[i] = pages[i].GetComponentsInChildren<RawImage>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            if (pageNumber < pages.Length-1) 
            {
                pageNumber++;
                flip();
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


}
