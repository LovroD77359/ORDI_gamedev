using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InstrStartUp : MonoBehaviour
{
    public GameObject Instructions;
    public int panelNumber = 0;
    public Button ForwardsButton;
    public Button BackwardsButton;
    public Button ContinueButton;
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    public GameObject Panel6;
    public GameObject pauseMenu;

    private GameObject[] panels;
    private PauseMenu pauseMenuScript;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        int levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
        Debug.Log(levelsCompleted);
        if (levelsCompleted == 0 && !PauseMenu.resetOccurred)
        {
            Instructions.SetActive(true);
            Panel1.gameObject.SetActive(true);
            Panel2.gameObject.SetActive(false);
            Panel3.gameObject.SetActive(false);
            Panel4.gameObject.SetActive(false);
            Panel5.gameObject.SetActive(false);
            Panel6.gameObject.SetActive(false);
            ForwardsButton.gameObject.SetActive(true);
            BackwardsButton.gameObject.SetActive(false);
            ContinueButton.gameObject.SetActive(false);
            panels = new GameObject[] { Panel1, Panel2, Panel3, Panel4, Panel5, Panel6 };
            pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
            pauseMenuScript.isMenuDisabled = true;
            StartCoroutine(waitAndPause());
        }
        else
        { 
            Instructions.SetActive(false);
        }

    }

    IEnumerator waitAndPause()
    {
        yield return new WaitForSeconds(0.75f);
        pauseMenuScript.Pause();
        if (!Instructions.activeInHierarchy)
            Instructions.SetActive(true);

        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.ignoreListenerPause = true;
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

    public void Continue()
    {
        Instructions.SetActive(false);
        pauseMenuScript.isMenuDisabled = false;
        pauseMenuScript.Resume();
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
            ContinueButton.gameObject.SetActive(false);
        }
        else if (panelNumber == panels.Length - 1)
        {
            ForwardsButton.gameObject.SetActive(false);
            BackwardsButton.gameObject.SetActive(true);
            ContinueButton.gameObject.SetActive(true);
        }
        else
        {
            ForwardsButton.gameObject.SetActive(true);
            BackwardsButton.gameObject.SetActive(true);
            ContinueButton.gameObject.SetActive(false);
        }
    }
}
