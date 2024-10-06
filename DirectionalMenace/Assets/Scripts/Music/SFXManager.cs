using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    [SerializeField] private AudioSource m_AudioPrefab; 
    public static SFXManager m_CurrentInstance;


    void Awake()
    {
        if (m_CurrentInstance == null)
        {
            m_CurrentInstance = this;
        }
    }


    public void PlaySFX(AudioClip l_AudioClip, Transform l_Entity, float l_volume)
    {
        //Instantiate the audioSource in the entity position
        AudioSource l_AS = Instantiate(m_AudioPrefab, l_Entity.position, Quaternion.identity); //rotation is indiferent
        //assign the audioclip
        l_AS.clip = l_AudioClip;
        //assign the volume
        l_AS.volume = l_volume;
        //play sound of the FX clip
        l_AS.Play();
        //Get the clip length
        float l_ClipLength = l_AS.clip.length;
        //destroy the clip
        Destroy(l_AS.gameObject, l_ClipLength);

    }
}
