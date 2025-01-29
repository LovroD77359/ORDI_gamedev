using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions_panel : MonoBehaviour
{
    public int panelNumber = 0;
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
        panelNumber++;
        updatePanels();
    }

    public void Backward() 
    {
        panelNumber--;
        updatePanels();
    }

    public void Back(bool isDelayed)
    {
        if (isDelayed)
        {
            StartCoroutine(waitForUpdate());
        }
        else
        {
            panelNumber = 0;
            updatePanels();
        }
    }

    IEnumerator waitForUpdate()
    {
        yield return new WaitForSeconds(0.75f);
        panelNumber = 0;
        updatePanels();
    }

    void updatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelNumber)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            } 
        }
        if (panelNumber == 0)
        {
            ForwardsButton.gameObject.SetActive(true);
            BackwardsButton.gameObject.SetActive(false);
        }
        else if (panelNumber == panels.Length - 1)
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
}
