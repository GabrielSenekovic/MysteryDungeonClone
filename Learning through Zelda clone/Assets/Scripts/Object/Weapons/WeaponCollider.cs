using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]

public class WeaponCollider : MonoBehaviour {

    public ItemType Type; //OOOOOOHHHH so this is how you decide if the weapon is a sword, club etc?? NO. It says which item it is only.
    protected Collider2D m_Collider;
    protected void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }


    protected void OnTriggerEnter2D(Collider2D collider)
        //If the collisionbox isnt a trigger, use OnCollisionEnter2D(Collision2D collision)
        //If the collisionbox is a trigger, us OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("hit " + collider.gameObject.name);
        AttackableBase attackable = collider.gameObject.GetComponent<AttackableBase>();
        //collision.gameObject.GetComponent gets the component of the gameobject this gameobject collides with somehow
        //Here it get attackablebase, that would also return attackablebush. In fact, it will return all derivatives
        //Debug.Log(collider.name, collider.gameObject);
        if (attackable != null)
        {
            attackable.OnHit(m_Collider, Type, 2);
            //So if the attackable doesnt return null, the OnHit function of the AttackableBase will run
            //It will only return null if the other gameobject has no attackablebase or derivative thereof
        }
    }
}
