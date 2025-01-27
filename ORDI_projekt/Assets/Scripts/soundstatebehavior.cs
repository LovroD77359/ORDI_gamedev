using UnityEngine;
using System.Collections; // Ovo omogućuje korištenje IEnumerator

public class SoundStateBehaviour : StateMachineBehaviour
{
    public AudioClip audioClip; // Zvuk koji se treba pustiti
    public float fadeDuration = 0.2f; // Trajanje fade in i fade out efekata
    public bool shouldLoop = false; // Određuje hoće li se zvuk petljati

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Preuzmi AudioSource s GameObjecta na kojem se nalazi Animator
        audioSource = animator.GetComponent<AudioSource>();

        if (audioSource != null && audioClip != null)
        {
            // Postavi AudioClip i loop
            audioSource.clip = audioClip;
            audioSource.loop = shouldLoop; // Postavi petljanje zvuka
            audioSource.volume = 0f; // Postavi glasnoću na 0 za fade in

            // Pokreni zvuk i fade in
            audioSource.Play();
            if (fadeCoroutine != null) animator.GetComponent<MonoBehaviour>().StopCoroutine(fadeCoroutine);
            fadeCoroutine = animator.GetComponent<MonoBehaviour>().StartCoroutine(FadeAudio(audioSource, 0f, 1f, fadeDuration));
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSource != null)
        {
            // Pokreni fade out i zaustavi zvuk nakon fade out-a
            if (fadeCoroutine != null) animator.GetComponent<MonoBehaviour>().StopCoroutine(fadeCoroutine);
            fadeCoroutine = animator.GetComponent<MonoBehaviour>().StartCoroutine(FadeAudio(audioSource, audioSource.volume, 0f, fadeDuration, stopAfterFade: true));
        }
    }

    private IEnumerator FadeAudio(AudioSource source, float startVolume, float targetVolume, float duration, bool stopAfterFade = false)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        source.volume = targetVolume;

        // Zaustavi zvuk ako je glasnoća smanjena na 0
        if (stopAfterFade && targetVolume == 0f)
        {
            source.Stop();
        }
    }
}
