 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTex;

    private void Start()
    {
        // Directly access the singleton instance of LevelLoader
        if (LevelLoader.instance == null)
        {
            Debug.LogError("LevelLoader instance not found!");
        }

        cursorSet();
    }

    void cursorSet()
    {
        CursorMode mode = CursorMode.ForceSoftware;
        int xspot = cursorTex.width / 4;
        int yspot = cursorTex.height / 8;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(cursorTex, hotSpot, mode);
        Cursor.visible = true;
    }

    public void LoadLevelSelect()
    {
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadNewLevel(1);
        }
        else
        {
            Debug.Log("LevelLoader reference is missing in MainMenu!");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
