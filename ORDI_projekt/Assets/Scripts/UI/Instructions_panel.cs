using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions_panel : MonoBehaviour
{

    int panel_number = 0;
    public Button ForwardsButton;
    public Button BackwardsButton;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    public GameObject Panel6;

    private GameObject[] panels;

    private void Start()
    {
        Panel1.gameObject.SetActive(true);
        Panel2.gameObject.SetActive(false);
        Panel3.gameObject.SetActive(false);
        Panel4.gameObject.SetActive(false);
        Panel5.gameObject.SetActive(false);
        Panel6.gameObject.SetActive(false);
        ForwardsButton.gameObject.SetActive(true);
        BackwardsButton.gameObject.SetActive(false);
        panels = new GameObject[] { Panel1, Panel2, Panel3, Panel4, Panel5, Panel6 };
    }

    public void Forward() 
    {
        panel_number++;
        updatePanels();
    }

    public void Backward() 
    {
        panel_number--;
        updatePanels();
    }

    public void Back() 
    {
        panel_number = 0;
        updatePanels();
    }

    void updatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panel_number)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            } 
        }
        if (panel_number == 0)
        {
            ForwardsButton.gameObject.SetActive(true);
            BackwardsButton.gameObject.SetActive(false);
        }
        else if (panel_number == panels.Length - 1)
        {
            ForwardsButton.gameObject.SetActive(false);
            BackwardsButton.gameObject.SetActive(true);
        }
        else
        {
            ForwardsButton.gameObject.SetActive(true);
            BackwardsButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
