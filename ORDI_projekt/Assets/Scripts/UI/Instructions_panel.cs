using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions_panel : MonoBehaviour
{

    int panel_number = 1;
    public Button ForwardsButton;
    public Button BackwardsButton;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;


    public void Forward() 
    {
        panel_number++;
    }

    public void Backward() 
    {
        panel_number--;
    }

    public void Back() 
    {
        panel_number = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (panel_number) 
        {
            case 1:
                Panel1.gameObject.SetActive(true);
                Panel2.gameObject.SetActive(false);
                Panel3.gameObject.SetActive(false);
                Panel4.gameObject.SetActive(false);
                ForwardsButton.gameObject.SetActive(true);
                BackwardsButton.gameObject.SetActive(false);
                break;
            case 2:
                Panel1.gameObject.SetActive(false);
                Panel2.gameObject.SetActive(true);
                Panel3.gameObject.SetActive(false);
                Panel4.gameObject.SetActive(false);
                ForwardsButton.gameObject.SetActive(true);
                BackwardsButton.gameObject.SetActive(true);
                break;
            case 3:
                Panel1.gameObject.SetActive(false);
                Panel2.gameObject.SetActive(false);
                Panel3.gameObject.SetActive(true);
                Panel4.gameObject.SetActive(false);
                ForwardsButton.gameObject.SetActive(true);
                BackwardsButton.gameObject.SetActive(true);
                break;
            case 4:
                Panel1.gameObject.SetActive(false);
                Panel2.gameObject.SetActive(false);
                Panel3.gameObject.SetActive(false);
                Panel4.gameObject.SetActive(true);
                ForwardsButton.gameObject.SetActive(false);
                BackwardsButton.gameObject.SetActive(true);
                break;

        }
    }
}
