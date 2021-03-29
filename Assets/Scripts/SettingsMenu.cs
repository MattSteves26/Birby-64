using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public static float volumebar;

    public AudioMixer audioMixer;
    public Slider slider;

    public TMPro.TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        int currentResolutionsIndex = 0;
        int i = 0;
        List<string> options = new List<string>();

        foreach (Resolution x in resolutions)
        {
            string option = x.width + "x" + x.height;
            options.Add(option);

            if(x.width == Screen.currentResolution.width && x.height == Screen.currentResolution.height)
            {
                currentResolutionsIndex = i;
            }
            i++;
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionsIndex;
        resolutionsDropdown.RefreshShownValue();

        slider.value = volumebar;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        volumebar = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
