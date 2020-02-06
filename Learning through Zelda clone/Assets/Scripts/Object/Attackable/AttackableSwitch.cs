using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableSwitch : AttackableBase
{
    private SpriteRenderer m_Renderer;
    void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void SwitchColor()
    {
        m_Renderer.color = new Color(255, 255, 0);
    }
    public override void OnHit(Collider2D collider, ItemType item, float damage)
    {
        Debug.Log("Switch has been collided with");
        SwitchColor();
    }
}
