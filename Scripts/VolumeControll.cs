using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControll : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerMusic;
    [SerializeField] private AudioMixerGroup _mixerSounds;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;

    public void OnSliderMusicValueChanged(float value)
    {
        _mixerMusic.audioMixer.SetFloat("MusicValue", value);
    }

    public void OnSliderSoundsValueChanged(float value)
    {
        _mixerSounds.audioMixer.SetFloat("SoundsValue", value);
    }

    private void OnEnable()
    {
        float value;
        _mixerMusic.audioMixer.GetFloat("MusicValue", out value);
        _musicSlider.value = value;
        _mixerSounds.audioMixer.GetFloat("SoundsValue", out value);
        _soundsSlider.value = value;
    }

    private void OnDisable()
    {
        float musicValue, soundsValue;
        _mixerMusic.audioMixer.GetFloat("MusicValue", out musicValue);
        _mixerSounds.audioMixer.GetFloat("SoundsValue", out soundsValue);
        SettingsData settings = new SettingsData(musicValue, soundsValue);
        SettingsData.SerializeSettings(settings);
    }
}
