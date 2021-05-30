using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _showTutorialToggle;

    private float _originalSoundVolume;
    private bool _isTutorialOn;
    private Tutorial _tutorial;


    private void Start()
    {
        _audioMixer.GetFloat("Volume", out _originalSoundVolume);
        _tutorial =  FindObjectOfType<Tutorial>();
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);

    }
   
    public void SetTutorial(bool isOn)
    {
        _tutorial.ToggleTutorial(isOn);
    }
}
