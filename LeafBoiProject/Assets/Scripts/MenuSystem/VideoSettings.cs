using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    public Resolution[] resolutions;
    public Dropdown resolutionDropdowns;

    private void Awake()
    {
        resolutions = new Resolution[3];
        resolutions[0].width = 1920;
        resolutions[0].height = 1080;
        resolutions[0].refreshRate = 60;
        resolutions[1].width = 1280;
        resolutions[1].height = 720;
        resolutions[1].refreshRate = 60;
        resolutions[2].width = 1024;
        resolutions[2].height = 576;
        resolutions[2].refreshRate = 60;

        resolutionDropdowns.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        options.Add("1920x1080");
        options.Add("1280x720");
        options.Add("1024x576");

        resolutionDropdowns.AddOptions(options);
        resolutionDropdowns.value = currentResolutionIndex;
        resolutionDropdowns.RefreshShownValue();
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        print(resolution);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
