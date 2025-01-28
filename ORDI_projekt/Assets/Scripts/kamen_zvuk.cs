using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnMovementWithTag : MonoBehaviour
{
    public AudioClip movingSound;        // Zvuk koji se pušta kad se objekt kreće
    public float minSpeedThreshold = 0.1f; // Minimalna brzina ispod koje se zvuk ne pušta
    public float fadeDuration = 0.2f;   // Trajanje fade in/out efekta
    public float maxVolume = 1f;        // Maksimalna glasnoća zvuka (0-1)
    public bool loopSound = true;       // Treba li se zvuk petljati

    private Rigidbody rb;               // Referenca na Rigidbody komponentu
    private AudioSource audioSource;    // Referenca na AudioSource komponentu
    private Coroutine fadeCoroutine;    // Referenca na aktivnu korutinu za fade
    private bool isPlaying = false;     // Status puštanja zvuka
    private bool isTouchedByPlayer = false; // Provjera dodira s objektom s odgovarajućim tagom

    private void Start()
    {
        // Dohvaćanje Rigidbody i AudioSource komponenata
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Postavljanje AudioSource postavki
        audioSource.clip = movingSound;
        audioSource.loop = loopSound;
        audioSource.volume = 0f; // Početna glasnoća je 0
        audioSource.playOnAwake = false; // Zvuk se ne pokreće automatski
    }

    private void Update()
    {
        // Provjera brzine objekta
        float speed = rb.velocity.magnitude;

        // Zvuk se reproducira samo ako se objekt kreće i dodiruje ga igrač
        if (speed > minSpeedThreshold && isTouchedByPlayer)
        {
            // Ako uvjeti zadovoljeni, pokreni fade in
            if (!isPlaying)
            {
                StartSound();
            }
        }
        else
        {
            // Ako uvjeti nisu zadovoljeni, pokreni fade out
            if (isPlaying)
            {
                StopSound();
            }
        }
    }

    private void StartSound()
    {
        isPlaying = true;

        // Pokreni fade in efekt
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    private void StopSound()
    {
        isPlaying = false;

        // Pokreni fade out efekt
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // Počni puštati zvuk
        }

        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, maxVolume, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = maxVolume; // Postavi glasnoću na maksimalnu
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f; // Postavi glasnoću na 0
        audioSource.Stop(); // Zaustavi zvuk
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Provjera je li objekt koji dodiruje ima tag "Player1" ili "Player2"
        if (collision.gameObject.CompareTag("Player") )
        {
            isTouchedByPlayer = true; // Postavi da je objekt dodirnut od strane igrača
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Ako objekt s tagom "Player1" ili "Player2" prestane dodirivati, isključujemo flag
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchedByPlayer = false;
        }
    }
}
