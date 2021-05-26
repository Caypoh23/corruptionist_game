using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Toggle _showTutorialToggle;
    [SerializeField] Toggle _isSoundOnToggle;

    private float _originalSoundVolume;
    private bool _isTutorialOn;
    private Tutorial _tutorial; 


    private void Start()
    {
        _audioMixer.GetFloat("Volume", out _originalSoundVolume);
        _tutorial =  FindObjectOfType<Tutorial>();
    }

    // called from canvas

    //for general sound
    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }
    // for music
    /*
    public void SetBgVolume(float volume)
    {
        _audioMixer.SetFloat("BgVolume", volume);
    }
    */
    // to turn off bg music
    public void SetSound(bool isSoundOff)
    {
        //float originalVolume = _audioMixer.GetFloat("BgVolume");
        if (isSoundOff)
        {
            _audioMixer.SetFloat("Volume", -80f);
        }
        else
        {
            _audioMixer.SetFloat("Volume", _originalSoundVolume);
        }

    }
    public void SetTutorial(bool isOn)
    {
        _tutorial.ToggleTutorial(isOn);
    }
}
