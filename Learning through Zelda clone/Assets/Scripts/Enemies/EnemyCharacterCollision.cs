using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour {
    CharacterEnemyGeneralControl m_control;
    public bool Flying;
    public GameObject thingICollidedWith;
    private void Awake()
    {
        m_control = GetComponentInParent<CharacterEnemyGeneralControl>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(Flying == false)
        {
          //  Debug.Log(gameObject.name + " collided with something" + collider);
            if (collider.CompareTag("Player")) 
            //CompareTag() is much faster that .Tag == yadayada
            {
                if(collider.gameObject.GetComponent<Character>() == null)
                {
              //      Debug.Log(collider.gameObject + " does not have a character component");
                    return;
                }
                m_control.OnHitCharacter(collider.gameObject.GetComponent<Character>());
            //    Debug.Log(gameObject.name + " collided with Player");

            }
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<AttackableEnemy>().OnHit(GetComponent<CircleCollider2D>(), ItemType.Sword, GetComponentInParent<CharacterEnemyGeneralControl>().AttackDamage);
            }
        }
        if (Flying == true)
        {
            if (collider.CompareTag("Head"))
            {
           // Debug.Log(gameObject.name + " collided with Player´s head");
            //Debug.Log(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.name);
            m_control.OnHitCharacter(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Character>()); //This isn't the most beautiful script in the world
            
            }
        }
    }
}
