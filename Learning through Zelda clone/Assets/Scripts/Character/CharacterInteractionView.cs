using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionView : MonoBehaviour {
    /*
    This script get the interactin model
    and uses it in order to animate the player picking things up
    */
    public Animator Animator;
    CharacterInteractionModel m_Model;

    private void Awake()
    {
        m_Model = GetComponent<CharacterInteractionModel>();
    }
    private void Update()
    {
        UpdateIsCarryingObject();
    }
    void UpdateIsCarryingObject()
    {
        Animator.SetBool("IsHolding", m_Model.IsCarryingObject());
    }
}
