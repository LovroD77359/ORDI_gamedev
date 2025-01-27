using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Metoda za fade in zvuka
    public void FadeIn(float duration)
    {
        StartCoroutine(FadeAudio(0f, 1f, duration));
    }

    // Metoda za fade out zvuka
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeAudio(audioSource.volume, 0f, duration));
    }

    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (targetVolume == 0f)
        {
            audioSource.Stop();
        }
    }
}
