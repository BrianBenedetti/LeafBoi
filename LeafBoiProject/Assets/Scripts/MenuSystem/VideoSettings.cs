﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdowns;

    private void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionDropdowns.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == resolutions[i].width)
            {
                currentResolutionIndex = i;
            }
        }

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
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
