using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{

    [SerializeField] private GameObject m_ObjectPrefab;

    private List<GameObject> m_PoolList;

    [SerializeField] private int m_InitialInstances;
    
    private void Awake()
    {
        m_PoolList = new List<GameObject>();
        for (int i = 0; i < m_InitialInstances; i++)
        {
            AddObjectToPool();
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject l_GO = Instantiate(m_ObjectPrefab);
        l_GO.SetActive(false);
        m_PoolList.Add(l_GO);
        return l_GO;
    }


    private GameObject GetAvailableGameObject()
    {
        for (int i = 0; i < m_PoolList.Count; i++)
        {
            if (!m_PoolList[i].activeInHierarchy) 
                return m_PoolList[i];
        }

        return m_PoolList[0];
    }

    public GameObject EnableObject()
    {
        GameObject l_GO = GetAvailableGameObject();
        
        m_PoolList.Remove(l_GO);
        m_PoolList.Add(l_GO);

        return l_GO;
    }
}
