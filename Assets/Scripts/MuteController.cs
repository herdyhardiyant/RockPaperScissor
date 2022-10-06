using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteController : MonoBehaviour
{
    // [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(SetMute);
        _toggle.isOn = Settings.GetMute() == 1;
    }
    

    public void SetMute(bool mute)
    {
        AudioListener.pause = mute;
        Settings.SetMute(mute);
    }
}
