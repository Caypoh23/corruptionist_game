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

    private float _currentVolumeValue;
 


    private void Start()
    {
        _currentVolumeValue = DataManager.Instance.LoadVolumeValue();

        slider.value = _currentVolumeValue;
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

    public void SaveVolume()
    {
        DataManager.Instance.SaveVolumeValue(_currentVolumeValue);
    }

  
}
