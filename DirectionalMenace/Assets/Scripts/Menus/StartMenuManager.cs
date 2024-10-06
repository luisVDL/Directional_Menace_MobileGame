using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject m_SettingsPanel;
    [SerializeField] private GameObject m_Transition;


    public void OpenSettings()
    {
        m_SettingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        m_SettingsPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        m_Transition.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    

}
