using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public RawImage fullscreenCheckmark;
    
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<Resolution> tempList = new List<Resolution>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Mathf.Round(((float)resolutions[i].width / (float)resolutions[i].height) * 100) == 178 && !tempList.Exists(res => res.width == resolutions[i].width))
            {
                tempList.Add(resolutions[i]);
            }
        }
        resolutions = tempList.ToArray();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            { 
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) 
    {
        Debug.Log(Mathf.Round(volume * 100));
        if (Mathf.Round(volume * 100) == 0)
        {
            audioMixer.SetFloat("volume", -80);
        }
        else
        {
            audioMixer.SetFloat("volume", 0.45f * Mathf.Round(volume * 100) - 45); 
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenCheckmark.enabled = !fullscreenCheckmark.enabled;
    }
}
