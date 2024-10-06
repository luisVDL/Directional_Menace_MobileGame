using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IRestartable
{

    [SerializeField] private List<GameObject> m_Hearts;

    [SerializeField] private UnityEvent m_PlayerLoses;
    
    [SerializeField] private AudioClip m_PlayerHurtClip;
    [SerializeField] private AudioClip m_PlayerDeathClip;
    
    private int m_Lives;
    
    
    
    void Start()
    {
        m_Lives = m_Hearts.Count;
        foreach (GameObject l_GO in m_Hearts)
        {
          l_GO.SetActive(true);  
        }
        
    }

    

    public void PlayerTakesDamage()
    {
        m_Lives -= 1;
        
        //deactivate heart
        
        //check if dead
        if (m_Lives == 0)
        {
            m_Hearts[m_Lives].SetActive(false);
            m_PlayerLoses.Invoke();
            SFXManager.m_CurrentInstance.PlaySFX(m_PlayerDeathClip, transform, 1f);
        }
        else
        {
            m_Hearts[m_Lives].SetActive(false); 
            SFXManager.m_CurrentInstance.PlaySFX(m_PlayerHurtClip, transform, 1f);
        }
    }


    public void Restart()
    {
        foreach (GameObject l_heart in m_Hearts)
        {
            l_heart.SetActive(true);
        }

        m_Lives = m_Hearts.Count;
    }
}
