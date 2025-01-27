using UnityEngine;
using System.Collections;

public class MovePlatformOnButtonPressPhys : MonoBehaviour
{
    public GameObject platform;          // Referenca na platformu koja se pomiče
    public float targetX;                // Ciljna pozicija X osi
    public float targetY;                // Ciljna pozicija Y osi
    public float targetZ;                // Ciljna pozicija Z osi
    public float moveSpeed = 2.0f;       // Brzina pomicanja platforme
    public AudioClip movingSound;        // Zvuk koji se pušta dok se platforma pomiče
    public bool enableLooping = true;    // Omogućava ili onemogućava loopanje zvuka
    public float fadeDuration = 0.06f;    // Vrijeme trajanja fade in/out efekta

    private Animator animator;
    private Rigidbody rb;
    private AudioSource audioSource;     // AudioSource za zvuk
    private float correctionOffset;
    private Vector3 initialPosition;     // Početna pozicija platforme
    private Vector3 targetPosition;      // Ciljna pozicija platforme
    private Vector3 vectorToTarget;
    private int buttonPressCount = 0;    // Status kretanja prema ciljnoj poziciji
    private Coroutine fadeCoroutine;     // Referenca na trenutno aktivnu korutinu za fade

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = platform.GetComponent<Rigidbody>();

        // Dodaj AudioSource komponentu ako već ne postoji
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = movingSound;
        audioSource.loop = enableLooping;  // Postavi loop na temelju postavke
        audioSource.volume = 0f;          // Početna glasnoća postavljena na 0 (tiho)

        correctionOffset = moveSpeed * 0.03f;

        // Spremamo početnu poziciju platforme
        initialPosition = platform.transform.position;
        // Definiramo ciljnu poziciju na temelju unesenih koordinata
        targetPosition = new Vector3(targetX, targetY, targetZ);

        vectorToTarget = (targetPosition - initialPosition).normalized;
    }

    private void FixedUpdate()
    {
        // Provjera je li platforma u pokretu
        bool isMoving = false;

        // Ako je platforma u pokretu prema cilju
        if (buttonPressCount > 0)
        {
            if (Vector3.Distance(platform.transform.position, targetPosition) < correctionOffset)
            {
                platform.transform.position = targetPosition;
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * vectorToTarget);
                isMoving = true;
            }
        }
        // Ako je platforma u povratku prema početnoj poziciji
        else if (buttonPressCount == 0)
        {
            if (Vector3.Distance(platform.transform.position, initialPosition) < correctionOffset)
            {
                platform.transform.position = initialPosition;
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.MovePosition(platform.transform.position + moveSpeed * Time.fixedDeltaTime * -vectorToTarget);
                isMoving = true;
            }
        }

        // Upravljanje zvukom
        HandleMovingSound(isMoving);
    }

    private void HandleMovingSound(bool isMoving)
    {
        if (isMoving)
        {
            // Ako se platforma kreće i zvuk nije već aktiviran, pokreni fade in
            if (!audioSource.isPlaying || audioSource.volume == 0f)
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeIn());
            }
        }
        else
        {
            // Ako platforma prestane s kretanjem, pokreni fade out
            if (audioSource.isPlaying && audioSource.volume > 0f)
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeOut());
            }
        }
    }

    private IEnumerator FadeIn()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); // Počni reproducirati zvuk ako nije već aktivan
        }

        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 1f, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f; // Postavi glasnoću na maksimalnu
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

    private void OnTriggerEnter(Collider other)
    {
        if (buttonPressCount == 0)
        {
            animator.SetBool("isPressed", true);
        }

        // Kada bilo koji objekt dotakne gumb, povećavamo button press count
        buttonPressCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        // Kada objekt napusti gumb, smanjujemo button press count
        buttonPressCount--;

        if (buttonPressCount == 0)
        {
            animator.SetBool("isPressed", false);
        }
    }
}
