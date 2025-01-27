using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    [System.Serializable]
    public class AnimationSound
    {
        public string animationName;
        public AudioClip audioClip;
    }

    public List<AnimationSound> animationSounds;

    private Dictionary<string, AudioClip> soundDictionary;
    private string currentAnimation;
    private Coroutine fadeCoroutine;

    void Start()
    {
        audioSource.volume = 0f;

        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var animSound in animationSounds)
        {
            soundDictionary[animSound.animationName] = animSound.audioClip;
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        foreach (var animSound in animationSounds)
        {
            if (stateInfo.IsName(animSound.animationName) && currentAnimation != animSound.animationName)
            {
                PlaySound(animSound.animationName);
                return;
            }
        }

        if (currentAnimation != null && !stateInfo.IsName(currentAnimation))
        {
            StopSound();
        }
    }

    private void PlaySound(string animationName)
    {
        if (soundDictionary.TryGetValue(animationName, out AudioClip clip))
        {
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                FadeIn(0.5f);
            }

            currentAnimation = animationName;
        }
    }

    private void StopSound()
    {
        FadeOut(0.5f);
        currentAnimation = null;
    }

    private void FadeIn(float duration)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(0f, 1f, duration));
    }

    private void FadeOut(float duration)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(audioSource.volume, 0f, duration));
    }

    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (targetVolume == 0f)
        {
            audioSource.Stop();
        }
    }
}
