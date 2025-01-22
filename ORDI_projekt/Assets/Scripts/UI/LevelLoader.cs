using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;  // Singleton instance

    private Animator animator;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate LevelLoader
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void LoadNewLevel(int buildIndex)
    {
        StartCoroutine(LoadLevel(buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelIndex);
        animator.SetTrigger("End");
    }
}