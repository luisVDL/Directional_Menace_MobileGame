using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Header("Spawner list")]
    [SerializeField] private GameObject m_SpawnerUp;
    private Vector2 m_SpawnerUpPosition;
    private Vector2 m_SpawnerUpDirection = new Vector2(0, -1);
    [SerializeField] private GameObject m_SpawnerDown;
    private Vector2 m_SpawnerDownPosition;
    private Vector2 m_SpawnerDownDirection= new Vector2(0, 1);
    [SerializeField] private GameObject m_SpawnerLeft;
    private Vector2 m_SpawnerLeftPosition;
    private Vector2 m_SpawnerLeftDirection  = new Vector2(1, 0);
    [SerializeField] private GameObject m_SpawnerRight;
    private Vector2 m_SpawnerRightPosition;
    private Vector2 m_SpawnerRightDirection = new Vector2(-1, 0);

    [Space(10)] 
    
    [Header("Enemy Stats")] 
    [SerializeField] private float m_InitialSpeed = 0.5f;

    private static float m_StaticInitialSpeed;
    [SerializeField] private float m_InitialEnemySpawnCooldown = 0.7f;
    private static float m_StaticInitialEnemySpawnCooldown;
    [SerializeField] private float m_EnemyDeathScore = 10f;
    private static float m_StaticEnemyDeathScore;
    private static float m_CurrentEnemySpeed;
    private static float m_CurrentSpawnCooldown;
    
    [Space(10)] 
    
    [Header("Enemy wave system")]
    [SerializeField] private float m_IncrementSpeed;
    private static float m_StaticIncrementSpeed;
    [SerializeField] private float m_IncrementEnemySpawnCooldown = 0.05f;
    [SerializeField] private PoolScript m_Pool;
    private float m_LastSpawn;
    private static int m_EnemiesDefeated = 0;
    private static int m_NextEnemiesGoal;
    [SerializeField] private int m_NEnemiesToIncrement;

    private static List<IRestartable> m_RestartElements;
    private static bool m_Paused;
    


    [Header("Score System")] 
    [SerializeField] private TextMeshProUGUI m_ScoreText;

    private static TextMeshProUGUI m_StaticScoreText;

    private static float m_CurrentScore;
    [SerializeField]private PlayerHealth m_PlayerHealth;
    private static PlayerHealth m_StaticPlayerHealth;

    private static EnemyManager m_CurrentInstance;

    [Header("Maximum values")] 
    [SerializeField] private float m_MaxSpeed;
    private static float m_StaticMaxSpeed;
    [SerializeField] private float m_MinCooldown = 0.4f;
    private static float m_StaticMinCooldown;

    [Header("Particles")] 
    [SerializeField] private PoolScript m_ParticlesPool;
    private static PoolScript m_StaticParticlesPool;

    [Header("Sounds")] 
    [SerializeField] private AudioClip m_EnemySpawnClip;
    [SerializeField] private AudioClip m_EnemyDeathClip;
    
    



    void Awake()
    {
        if (m_CurrentInstance == null)
        {
            m_CurrentInstance = this;
        }
    }
    
    public static EnemyManager getInstance()
    {
        if (m_CurrentInstance == null)
        {
            m_CurrentInstance = new EnemyManager();
        }
       
        return m_CurrentInstance;
        
    }
    void Start()
    {
        m_StaticParticlesPool = m_ParticlesPool;
        
        m_StaticIncrementSpeed = m_IncrementSpeed;
        m_StaticMaxSpeed = m_MaxSpeed;
        m_StaticMinCooldown = m_MinCooldown;
        m_StaticInitialSpeed = m_InitialSpeed;
        m_StaticInitialEnemySpawnCooldown = m_InitialEnemySpawnCooldown;
        
        
        //spawners position
        m_SpawnerUpPosition = m_SpawnerUp.transform.position;
        m_SpawnerDownPosition = m_SpawnerDown.transform.position;
        m_SpawnerLeftPosition = m_SpawnerLeft.transform.position;
        m_SpawnerRightPosition = m_SpawnerRight.transform.position;
        m_RestartElements = new List<IRestartable>();
        m_StaticEnemyDeathScore = m_EnemyDeathScore;
        m_StaticScoreText = m_ScoreText;
        m_StaticPlayerHealth = m_PlayerHealth;
        
        
        setStartVariables();
        
    }

    private void setStartVariables()
    {
        
        m_CurrentEnemySpeed = m_StaticInitialSpeed;
        m_CurrentSpawnCooldown = m_StaticInitialEnemySpawnCooldown;
        m_EnemiesDefeated = 0;
        m_NextEnemiesGoal = m_NEnemiesToIncrement;

        
        m_Paused = false;
        
        
        
        //time 
        m_LastSpawn = Time.time;
    }
    
    
    
    void Update()
    {
        if (m_LastSpawn + m_CurrentSpawnCooldown < Time.time && !m_Paused)
        {
            m_LastSpawn = Time.time;
            SpawnEnemy();

        }
    }
    
    private void SpawnEnemy()
    {
        GameObject l_GO = m_Pool.EnableObject();
        EnemyScript l_Enemy = l_GO.GetComponent<EnemyScript>();
        int l_random = Random.Range(0,4);

        switch (l_random)
        {
            case 0: //UP
                l_Enemy.Spawn(m_SpawnerUpDirection, m_CurrentEnemySpeed, m_SpawnerUpPosition);
                break;
            case 1: //DOWN
                l_Enemy.Spawn(m_SpawnerDownDirection, m_CurrentEnemySpeed, m_SpawnerDownPosition);
                break;
            case 2: // LEFT
                l_Enemy.Spawn(m_SpawnerLeftDirection, m_CurrentEnemySpeed, m_SpawnerLeftPosition);
                break;
            case 3: //RIGHT
                l_Enemy.Spawn(m_SpawnerRightDirection, m_CurrentEnemySpeed, m_SpawnerRightPosition);
                break;
        }
        SFXManager.m_CurrentInstance.PlaySFX(m_EnemySpawnClip, l_Enemy.transform, 1f);
    }
    
    public void EnemyDefeated(Transform l_Enemy)
    {
        SFXManager.m_CurrentInstance.PlaySFX(m_EnemyDeathClip, l_Enemy.transform, 1f);
        SpawnParticles(l_Enemy.position);
        
        m_EnemiesDefeated += 1;
        IncreaseDifficulty();
        m_CurrentScore += m_StaticEnemyDeathScore;
        m_StaticScoreText.SetText("SCORE "+ m_CurrentScore);
    }

    private void SpawnParticles(Vector2 l_position)
    {
        
        GameObject l_GO = m_StaticParticlesPool.EnableObject();
        l_GO.transform.position = l_position;
        l_GO.SetActive(true);
        ParticleSystem l_PS = l_GO.GetComponent<ParticleSystem>();
        l_PS.Play();
    }

    private void IncreaseDifficulty()
    {
        if (m_EnemiesDefeated >= m_NextEnemiesGoal)
        {
            m_NextEnemiesGoal += m_NEnemiesToIncrement;
            int l_random = Random.Range(0,2);

            if (l_random == 1)
            {
                m_CurrentEnemySpeed += m_StaticIncrementSpeed;
                m_CurrentEnemySpeed = Math.Min(m_CurrentEnemySpeed, m_StaticMaxSpeed);
                
            }
            else
            {
                m_CurrentSpawnCooldown -= m_IncrementEnemySpawnCooldown;
                m_CurrentSpawnCooldown = Math.Max(m_CurrentSpawnCooldown, m_StaticMinCooldown);
                
            }
        }
    }

    public float GetScore()
    {
        return m_CurrentScore;
    }

    public void PlayerTakesDamage()
    {
        m_StaticPlayerHealth.PlayerTakesDamage();
    }

    public void AddRestartElement(IRestartable l_element)
    {
        if (!m_RestartElements.Contains(l_element))
        {
            m_RestartElements.Add(l_element);
        }
    }

    public void RestartGame()
    {
        foreach (IRestartable l_element in m_RestartElements)
        {
            l_element.Restart();
        }

        PauseEnemies(false);
        m_CurrentScore = 0;
        setStartVariables();
        Time.timeScale=1.0f;
        m_StaticScoreText.SetText("SCORE "+ m_CurrentScore);
    }

    public void PauseEnemies(bool l_Pause)
    {
        m_Paused = l_Pause;
        foreach (IRestartable l_element in m_RestartElements)
        {
            EnemyScript l_Enemy = l_element as EnemyScript;
            if (l_Enemy != null)
            {
                l_Enemy.PauseEnemy(m_Paused);
            }
        }
    }
    
}
