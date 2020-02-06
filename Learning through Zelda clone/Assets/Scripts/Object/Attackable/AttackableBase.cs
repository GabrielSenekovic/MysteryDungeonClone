using UnityEngine;
using System.Collections;

public class AttackableBase : MonoBehaviour {

	public virtual void OnHit(Collider2D collider, ItemType item, float damage)
        //ItemType gets what item has hit us
        //virtual means a subscript can overwrite it
    {
        Debug.LogWarning("No OnHit Event Setup for " + gameObject.name, gameObject);
    }
}
