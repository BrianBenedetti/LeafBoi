using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private float _masterVol;
    private float _musicVol;
    private float _sfxVol;

    private float _masterFrac;
    private float _musicFrac;
    private float _sfxFrac;

    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        _masterFrac = masterSlider.value;
        _masterVol = (_masterFrac * 80) - 80;
        audioMixer.SetFloat("MasterVol", _masterVol);
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        _musicFrac = musicSlider.value;
        _musicVol = ((_masterFrac * _musicFrac) * 80) - 80;
        audioMixer.SetFloat("MusicVol", _musicVol);
    }

    public void SetSFXVolume()
    {
        _sfxFrac = sfxSlider.value;
        _sfxVol = ((_masterFrac * _sfxFrac) * 80) - 80;
        audioMixer.SetFloat("SFXVol", _sfxVol);

    }
}
