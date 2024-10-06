using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    public void GoToGame()
    {
        StartCoroutine(WaitForNoFuckingReason(true));

    }

    IEnumerator WaitForNoFuckingReason(bool l_start)
    {
        print("Start to wait");
        yield return new WaitForSeconds(0.2f);
        if (l_start)
        {
            print("Start");
            SceneManager.LoadScene("Game"); 
        }
        else
        {
            print("AAAAAA");
            SceneManager.LoadScene("StartMenu");
        }
        print("End");
        
    }

    public void TransitionGameStart()
    {
        m_Animator.SetBool("End", false);
        m_Animator.SetBool("Start", true);
    }

    public void TransitionGameEnd()
    {
        m_Animator.SetBool("Start", false);
        m_Animator.SetBool("End", true);
       
    }

    public void GoToStartMenu()
    {
        print("Go to start menu");
        StartCoroutine(WaitForNoFuckingReason(false));
    }
}