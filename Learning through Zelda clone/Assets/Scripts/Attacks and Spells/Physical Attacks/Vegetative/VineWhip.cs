using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWhip : AttackParent
{
    float vineLength = 5;
    public override void ExecuteAttack(GameObject me)
    {
        BoxCollider2D attackCollider = me.transform.parent.GetComponentInParent<CharacterAdvancedMovementModel>().vine;
        // attackCollider.enabled = true;
        Vector3 direction = me.transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        if (direction != Vector3.zero)
        {
            attackCollider.GetComponent<Vine>().SetVineSize(vineLength);
            attackCollider.GetComponent<Vine>().expanding = true;
        }
    }
}
