using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        volumeSlider.value = Settings.GetVolume();
    }

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(VolumeChangeHandler);
    }

    private void VolumeChangeHandler(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        Settings.SetVolume(value);
    }

}