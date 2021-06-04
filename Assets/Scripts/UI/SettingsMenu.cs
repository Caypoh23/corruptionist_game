using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    private float _originalSoundVolume;
    private float _currentVolumeValue = -50;
 

    private void Awake()
    {
        _currentVolumeValue = DataManager.Instance.LoadVolumeValue();
    
        slider.value = _currentVolumeValue;
    }

    private void Start()
    {
        audioMixer.SetFloat("Volume", _currentVolumeValue);
    }

    private void Update()
    {
        //Debug.Log(_currentVolumeValue);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        _currentVolumeValue = volume;
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveVolumeValue(_currentVolumeValue);
    }

  
}
