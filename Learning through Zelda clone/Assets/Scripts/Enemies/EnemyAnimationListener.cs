using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationListener : MonoBehaviour {

    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;
    public BoxCollider2D MyCollider;

    public void OnAttackStarted(AnimationEvent animationEvent)
    {
        if (MovementModel != null)
        {
            MovementModel.OnAttackStarted(); //This says to do the OnAttackStarted if the movementmodel returns null
            //This will make the MovementModel know when you are attacking or not, so it can decide whether you can attack or not
        }
        if (MovementView != null)
        {
            MovementView.OnAttackStarted(); //This will turn on and off the visuals of the weapon
        }
    }

    public void OnAttackFinished()
    {
        if (MovementModel != null)
        {
            MovementModel.OnAttackFinished(); //I need to separate these two. Into OnAttackFinished1 and OnAttackFinished2
        }
        if (MovementView != null)
        {
            MovementView.OnAttackFinished();
        }
    }
    public void OnHurt()
    {
        MovementModel.m_IsTakingDamage = true;
    }
    public void NormalizeBoxCollider()
    {
        if(MyCollider != null)
        {
            MyCollider.offset = new Vector2(0f, -0.3125f);
            MyCollider.enabled = false;
        }
    }
    public void UnFreeze()
    {
        MovementModel.m_IsTakingDamage = false; //thats here because it needs to be able to attack again
        GetComponentInParent<CharacterMovementModel>().SetFrozen(false, false, false);
    }
}
