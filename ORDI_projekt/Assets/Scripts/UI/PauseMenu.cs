using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static bool resetOccurred = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject InstructionsMenuUI;
    public bool isMenuDisabled = false;

    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuDisabled) 
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else 
            {
                Pause();;
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        InstructionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;

#if UNITY_EDITOR
        var gameWindow = EditorWindow
            .GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        gameWindow.Focus();
        gameWindow.SendEvent(new Event
        {
            button = 0,
            clickCount = 1,
            type = EventType.MouseDown,
            mousePosition = gameWindow.rootVisualElement.contentRect.center
        });
#endif
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;  // Lock cursor to the center
    }

    public void Pause() 
    {
        if (!isMenuDisabled)
        {
            PauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        IsGamePaused = true;

        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;  // Free the cursor
    }

    public void MainMenu()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(0);  // 0 is the build index for Title Page
        }
        else
        {
            Debug.LogError("LevelLoader instance not found! Loading scene directly.");
            SceneManager.LoadScene("Title page");
        }

        IsGamePaused = false;
        Time.timeScale = 1f;  // Ensure time resumes correctly
    }

    public void Reset()
    {
        Scene sceneIndex = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(sceneIndex.buildIndex);
        resetOccurred = true;
    }

    IEnumerator waitAndResetBool()
    {
        yield return new WaitForSeconds(0.1f);
        resetOccurred = false;
    }
}
