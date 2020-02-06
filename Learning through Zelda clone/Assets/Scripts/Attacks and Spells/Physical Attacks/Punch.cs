using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : AttackParent
{

    public override void ExecuteAttack(GameObject me)
    {
        me.transform.parent.GetComponentInParent<CharacterMovementView>().Animator.SetTrigger("Punch");
        me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().enabled = true;
        Vector3 direction = me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().GetComponent<CharacterMovementModel>().GetFacingDirection();
        if (direction != Vector3.zero)
        {
            if (direction.x != 1 || direction.y != 1)
            {
                if (direction.x >= 0.5 & direction.y <= 0.5)
                {
                    //east
                    me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset = new Vector2();
                }
                else if (direction.x <= -0.5 & direction.y >= -0.5)
                {
                    //west
                    me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset = new Vector2();
                }
                else if (direction.x <= 0.5 & direction.y >= 0.5)
                {
                    //up
                    me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset = new Vector2(0.25f, 0.25f);
                }
                else if (direction.x >= -0.5 & direction.y <= -0.5)
                {
                    //down
                    me.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset = new Vector2(-0.25f, -0.625f);
                }
                else
                {
                    Debug.Log(direction);
                    Debug.Log("Code has no fucking idea whats going on");
                }
            }

        }
        me.transform.parent.GetComponentInParent<CharacterMovementModel>().SetFrozen(true, true, false);
    }
}
