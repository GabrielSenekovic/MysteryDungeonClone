using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBatControl : CharacterEnemyGeneralControl
{

    override public void Update()
    {
        UpdateDirection();
    }
    void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;
        if (m_IsCharacterInRange != null)
        {
            //direction = transform.position - m_IsCharacterInRange.transform.position; makes the bat escape from you
            direction = m_IsCharacterInRange.transform.position - transform.position;
            direction.Normalize();
        }
        if(AttackableEnemy != null && AttackableEnemy.GetHealth()<=0)
        {
            direction = Vector2.zero;
        }
        SetDirection(direction);
    }
}
