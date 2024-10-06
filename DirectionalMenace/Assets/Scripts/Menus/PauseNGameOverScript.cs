using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseNGameOverScript : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject m_SettingsPanel;
    
    [Header("Events")]
    [SerializeField] private UnityEvent m_RestartEvent;
    [SerializeField] private UnityEvent m_DeathMenuShow;

    private EnemyManager m_Manager;


    void Start()
    {
        m_Manager = EnemyManager.getInstance();
    }
    



    public void OpenPauseMenu()
    {
        //Time.timeScale = 0.0f;
        m_Manager.PauseEnemies(true);
        m_PausePanel.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        m_PausePanel.SetActive(false); 
        m_Manager.PauseEnemies(false);
        //Time.timeScale = 1.0f;
    }


    public void OpenSettingsMenu()
    {
        m_SettingsPanel.SetActive(true);
    }
    
    public void CloseSettingsMenu()
    {
        m_SettingsPanel.SetActive(false);
    }
    
    public void GameOverShow()
    {
        m_GameOverPanel.SetActive(true);
        m_Manager.PauseEnemies(true);
        m_DeathMenuShow.Invoke();
        
    }

    public void RestartGame()
    {
        m_GameOverPanel.SetActive(false);
        m_PausePanel.SetActive(false);
        m_Manager.RestartGame();
        m_RestartEvent.Invoke();

    }

    public void GoToStart()
    {
        m_Manager.RestartGame();
        m_RestartEvent.Invoke();
        SceneManager.LoadScene("StartMenu");
    }
}
