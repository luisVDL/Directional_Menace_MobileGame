using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IRestartable
{
    private float m_Speed;
    private Vector2 m_Direction;
    private Rigidbody2D m_RB;

    private float m_Damage;
    private bool m_Pause;

    [SerializeField] private ParticleSystem m_SpawnParticles;


    private EnemyManager m_Manager;
    
    
    void Awake()
    {
        m_Manager = EnemyManager.getInstance();
        m_RB = GetComponent<Rigidbody2D>();
        m_Speed = 0;
        m_Direction = new Vector2(0, 0);
    }
    
    


    void Update()
    {
        if (!m_Pause)
        {
            m_RB.position += m_Direction * m_Speed;
        }
        
    }

    public void Spawn(Vector2 l_Direction, float l_Speed, Vector2 l_Position)
    {
        m_Manager.AddRestartElement(this);
        m_Pause = false;
        transform.position = l_Position;
        m_Speed = l_Speed;
        m_Direction = l_Direction;
        gameObject.SetActive(true);
        m_SpawnParticles.Play();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //remove player health
            m_Manager.PlayerTakesDamage();
            //deactivate the GameObject
            gameObject.SetActive(false);
        }
        else if (other.tag == "Barrier")
        {
            m_Manager.EnemyDefeated(transform);
            gameObject.SetActive(false);
        }
    }

    public void Restart()
    {
        gameObject.SetActive(false);
    }

    public void PauseEnemy(bool l_Pause)
    {
        m_Pause = l_Pause;
    }
}
