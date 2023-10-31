using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Sound", fileName = "SoundSettingsPreset")]
public class SoundSetting : ScriptableObject
{
    public AudioMixer AudioMixer;
    // Start is called before the first frame update
    [Header("MasterVolume")]
    public string MasterVolumeName = "MasterVolume";
    [Range(-80, 20)]
    public float MasterVolume;

    [Header("MusicVolume")]
    public string MusicVolumeName = "MusicVolume";
    [Range(-80, 20)]
    public float MusicVolume;

    [Header("MasterSFXVolume")]
    public string MasterSFXVolumeName = "MasterSFXVolume";
    [Range(-80, 20)]
    public float MasterSFXVolume;

    [Header("SFXVolume")]
    public string SFXVolumeName = "SFXVolume";
    [Range(-80, 20)]
    public float SFXVolume;

    [Header("UIVolume")]
    public string UIVolumeName = "UIVolume";
    [Range(-80, 20)]
    public float UIVolume;

}
