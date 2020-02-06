using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tackle : AttackParent
{

    public override void ExecuteAttack(GameObject me)
    {
        me.transform.parent.GetComponentInParent<CharacterMovementView>().Animator.SetTrigger("Tackle");
        BoxCollider2D attackCollider = me.transform.parent.GetComponentInParent<CharacterAdvancedMovementModel>().physicalAttack;
        attackCollider.enabled = true;
        Vector3 direction = me.transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        if (direction != Vector3.zero)
        {
            if(direction.x == 1 && direction.y == 0)
            {
                //east
                attackCollider.offset = new Vector2(0.5f, 0);
            }
            else if (direction.x == -1 && direction.y == 0)
            {
                //west
                attackCollider.offset = new Vector2(-0.5f, 0);
            }
            else if (direction.x == 0 && direction.y == 1)
            {
                //north
                attackCollider.offset = new Vector2(0, 0.5f);
            }
            else if (direction.x == 0 && direction.y == -1)
            {
                //south
                attackCollider.offset = new Vector2(0, -0.5f);
               
            }

        }
    }
}
