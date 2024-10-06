using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private GameObject m_Barrier_UP;
    [SerializeField] private GameObject m_Barrier_DOWN;
    [SerializeField] private GameObject m_Barrier_LEFT;
    [SerializeField] private GameObject m_Barrier_RIGHT;
    
    

    // Start is called before the first frame update
    void Start()
    {
       m_Barrier_UP.SetActive(false); 
       m_Barrier_DOWN.SetActive(false); 
       m_Barrier_LEFT.SetActive(false); 
       m_Barrier_RIGHT.SetActive(false); 
    }


    public void BarrierUp()
    {
        m_Barrier_UP.SetActive(true);
        m_Barrier_DOWN.SetActive(false); 
        m_Barrier_LEFT.SetActive(false); 
        m_Barrier_RIGHT.SetActive(false);
    }

    public void BarrierDown()
    {
        m_Barrier_UP.SetActive(false); 
        m_Barrier_DOWN.SetActive(true); 
        m_Barrier_LEFT.SetActive(false); 
        m_Barrier_RIGHT.SetActive(false); 
    }

    public void BarrierLeft()
    {
        m_Barrier_UP.SetActive(false); 
        m_Barrier_DOWN.SetActive(false); 
        m_Barrier_LEFT.SetActive(true); 
        m_Barrier_RIGHT.SetActive(false); 
    }
    
    public void BarrierRight()
    {
        m_Barrier_UP.SetActive(false); 
        m_Barrier_DOWN.SetActive(false); 
        m_Barrier_LEFT.SetActive(false); 
        m_Barrier_RIGHT.SetActive(true); 
    }
}
