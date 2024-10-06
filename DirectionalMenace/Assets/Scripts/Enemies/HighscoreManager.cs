using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_HighscoreText;
    [SerializeField] private TextMeshProUGUI m_CurrentScoreText;
    
    
    private EnemyManager m_Manager;


    void Start()
    {
        m_Manager = EnemyManager.getInstance();
    }
    

    public void ShowScore()
    {
        float l_Highscore=PlayerPrefs.GetFloat("Highscore", 0.0f);
        float l_CurrentScore = m_Manager.GetScore();

        if (l_Highscore < l_CurrentScore)
        {
            l_Highscore = l_CurrentScore;
            PlayerPrefs.SetFloat("Highscore", l_Highscore);
        }
        m_HighscoreText.SetText(""+ l_Highscore);
        m_CurrentScoreText.SetText(""+ l_CurrentScore);
        
    }
}
