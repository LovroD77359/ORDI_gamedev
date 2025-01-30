using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    [System.Serializable]
    public class AnimationSound
    {
        public string animationName;
        public AudioClip soundClip;
    }

    public List<AnimationSound> animationSounds = new List<AnimationSound>();

    private Dictionary<string, AudioClip> soundMap;
    private string lastPlayedAnimation = "";

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // Pretvaranje liste u dictionary radi bržeg pretraživanja
        soundMap = new Dictionary<string, AudioClip>();
        foreach (var animSound in animationSounds)
        {
            soundMap[animSound.animationName] = animSound.soundClip;
        }

        // Pokreni korutinu koja provjerava animacije svaki frame
        StartCoroutine(CheckAnimationLoop());
    }

    private IEnumerator CheckAnimationLoop()
    {
        while (true)
        {
            string currentAnimation = GetCurrentAnimation();

            if (!string.IsNullOrEmpty(currentAnimation) && currentAnimation != lastPlayedAnimation)
            {
                lastPlayedAnimation = currentAnimation;
                PlayAnimationSound(currentAnimation);
            }

            yield return null; // Provjera se događa svaki frame
        }
    }

    private string GetCurrentAnimation()
    {
        if (animator == null || animator.runtimeAnimatorController == null) 
            return "";

        for (int i = 0; i < animator.layerCount; i++) // Provjeri sve slojeve animacije
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(i);

            if (stateInfo.length == 0 || animator.GetCurrentAnimatorClipInfoCount(i) == 0)
                continue;

            return animator.GetCurrentAnimatorClipInfo(i)[0].clip.name;
        }

        return "";
    }

    private void PlayAnimationSound(string animationName)
    {
        if (soundMap.TryGetValue(animationName, out AudioClip clip) && clip != null)
        {
            audioSource.PlayOneShot(clip); // OneShot osigurava da zvuk završi iako se animacija promijeni
        }
    }
}
