using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider audioSound;
    public Slider audioMusic;
    [SerializeField] float fMusic = -20;
    [SerializeField] float fSound = -20;
    // Start is called before the first frame update
    void Start()
    {
        float music = GetVolumeMusic();
        float sound = GetVolumeSound();
        audioMusic.value = music;
        audioSound.value = sound;
        UpdateMusic(music);
        UpdateSound(sound);
    }
    // Update is called once per frame
    public void UpdateMusic(float value)
    {
        audioMixer.SetFloat(ConstanerGame.KeyMusic, value);
        OnStoreMusic(value);
    }
    public void UpdateSound(float value)
    {
        audioMixer.SetFloat(ConstanerGame.KeySound, value);
        OnStoreSound(value);
    }
    private void OnStoreMusic(float value)
    {
        PlayerPrefs.SetFloat(ConstanerGame.KeyMusic, value);
    }
    private void OnStoreSound(float value)
    {
        PlayerPrefs.SetFloat(ConstanerGame.KeySound, value);
    }
    float GetVolumeMusic()
    {
        return PlayerPrefs.GetFloat(ConstanerGame.KeyMusic, fMusic);
    }
    float GetVolumeSound()
    {
        return PlayerPrefs.GetFloat(ConstanerGame.KeySound, fSound);
    }
}
