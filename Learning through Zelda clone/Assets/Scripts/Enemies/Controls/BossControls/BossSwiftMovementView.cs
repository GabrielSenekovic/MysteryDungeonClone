using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwiftMovementView : CharacterMovementView
{

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_MovementModel.SetDirection(new Vector2(0,-1));

        if (Animator == null)
        {
            Debug.LogError("Character animator not setup!");
            enabled = false;
        }
    }

    override public void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetFacingDirection();
        if (direction != Vector3.zero)
        {    
            if (direction.x != 1 || direction.y != 1)
            {
                Animator.SetFloat("DirectionX", direction.x);
                Animator.SetFloat("DirectionY", direction.y);
            }

        }
        Animator.SetBool("IsMoving", m_MovementModel.IsMoving());
    }
}
