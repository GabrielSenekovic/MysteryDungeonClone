using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackParent : AttackParent
{
    public override void ExecuteAttack(GameObject me)
    {
       // Debug.Log("Shoot!");
        me.transform.parent.GetComponentInParent<CharacterMovementView>().Animator.SetBool("Cast", true);
        me.transform.parent.GetComponentInParent<CharacterMovementView>().SetBoolFalse("Cast", GetComponent<AttackIdentifier>());
        me.transform.parent.GetComponentInParent<CharacterMovementModel>().SetFrozen(true, true, false);
        GetComponent<ProjectileSpawner>().OnSpawnByCharacter(me);
    }
}
