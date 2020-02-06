using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthModel : MonoBehaviour
{
    public float StartingHealth;
    [SerializeField]
    private float m_MaximumHealth;
    [SerializeField]
    private float m_Health;
    private void Start()
    {
        m_Health = StartingHealth;
        m_MaximumHealth = StartingHealth;
    }
    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.T))
        {
            DealDamage(10);
            Debug.Log("Debug damage has been inflicted");
        }
        if (m_Health > m_MaximumHealth)
        {
            m_Health = m_MaximumHealth;
        }
        else if(m_Health < 0)
        {
            m_Health = 0;
        }
    }
    public float GetHealth()
    {
        return m_Health;
    }
    public float GetMaximumHealth()
    {
        return m_MaximumHealth;
    }
    public float GetHealthPercentage()
    {
        return m_Health / m_MaximumHealth;
    }
    public void DealDamage(float damage)
    {
        if(m_Health <= 0)
        {
            return;
        }
        m_Health -= damage;
        if(m_Health <= 0)
        {
            Debug.Log("Fuck, you died");
        }
    }
}
