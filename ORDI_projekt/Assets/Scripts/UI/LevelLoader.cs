using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;  // Singleton instance

    public float transitionTime = 1f;
    public AudioClip mainTheme;
    public AudioClip caveTheme;
    public AudioClip treeTheme;

    private Animator animator;
    private AudioSource audioSource;
    private float defaultVolume;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate LevelLoader
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // Persist across scenes
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
        audioSource.ignoreListenerPause = true;
    }

    public void LoadNewLevel(int buildIndex)
    {
        StartCoroutine(LoadLevel(buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        float startVolume = audioSource.volume;
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        AudioClip newClip = null;
        if ((levelIndex == 0 && currentLevelIndex != 1) || levelIndex == 8)
        {
            newClip = mainTheme;
        }
        else if (levelIndex == 1 && currentLevelIndex != 0)
        {
            newClip = mainTheme;
        }
        else if (levelIndex == 2 || (levelIndex == 4 && currentLevelIndex != 3))
        {
            newClip = caveTheme;
        }
        else if (levelIndex == 5 || (levelIndex == 7 && currentLevelIndex != 6))
        {
            newClip = treeTheme;
        }
        float newVolume = defaultVolume;
        if ((levelIndex == 0 && currentLevelIndex != 1) || (levelIndex == 1 && currentLevelIndex != 0) || levelIndex == 8 || levelIndex == 9)
        {
            newVolume *= 1.5f;
        }
        if (levelIndex == 2 || levelIndex == 5)
        {
            newVolume *= 0.5f;
        }

        animator.SetTrigger("Start");
        for (int i = 9; i >= 0; i--)
        {
            yield return new WaitForSeconds(transitionTime / 10);
            if (newClip != null)
            {
                audioSource.volume = startVolume * (i / 5);
            }
        }
        SceneManager.LoadSceneAsync(levelIndex);
        yield return new WaitForSeconds(transitionTime / 10);
        
        animator.SetTrigger("End");
        if (newClip != null)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
        audioSource.volume = newVolume;
    }
}