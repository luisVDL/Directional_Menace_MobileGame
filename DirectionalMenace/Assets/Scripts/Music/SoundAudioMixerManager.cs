using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundAudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;

    [Header("Sliders")] 
    [SerializeField] private Slider m_MasterVS;
    [SerializeField] private Slider m_MusicVS;
    [SerializeField] private Slider m_SFXVS;



    void Awake()
    {
        //set volumes
        setMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        setMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        setSFXVolume(PlayerPrefs.GetFloat("SoundFXVolume", 1f));
        
        //set sliders values
        m_MasterVS.SetValueWithoutNotify(PlayerPrefs.GetFloat("MasterVolume"));
        m_MusicVS.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVolume"));
        m_SFXVS.SetValueWithoutNotify(PlayerPrefs.GetFloat("SoundFXVolume"));
    }

    public void setMasterVolume(float l_volume)
    {
        float l_value = Mathf.Log10(l_volume) * 20f;
        m_AudioMixer.SetFloat("MasterVolume", l_value);
        PlayerPrefs.SetFloat("MasterVolume", l_volume);
    }
    
    public void setSFXVolume(float l_volume)
    {
        float l_value = Mathf.Log10(l_volume) * 20f;
        m_AudioMixer.SetFloat("SoundFXVolume", l_value);
        PlayerPrefs.SetFloat("SoundFXVolume", l_volume);
    } 
    
    public void setMusicVolume(float l_volume)
    {
        float l_value = Mathf.Log10(l_volume) * 20f;
        m_AudioMixer.SetFloat("MusicVolume", l_value);
        PlayerPrefs.SetFloat("MusicVolume", l_volume);
    }
    
}
