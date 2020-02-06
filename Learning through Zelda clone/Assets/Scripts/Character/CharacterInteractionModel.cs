using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Character))] //Now if you add an interaction model on a gameobject it checks if theres a character component. If not, it adds one

public class CharacterInteractionModel : MonoBehaviour
{
    private InteractablePickup m_PickedUpObject;
    private Character m_Character;
    private Collider2D m_Collider;
    [SerializeField]
    private Collider2D m_InteractableCollider;
    private CharacterAdvancedMovementModel m_MovementModel;
    public bool m_IsCloseEnoughToInteract;
    public InteractableBase Interactable; //This is where the thing you interact with end up. This doesnt necessarily have to be public 
    [SerializeField]
    private CharacterNotification interactableNotification;
 
    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_Collider = GetComponent<Collider2D>();
        m_MovementModel = GetComponent<CharacterAdvancedMovementModel>();
    }

    public void OnInteract()
    {
        if(IsCarryingObject()==true)
        {
            ThrowCarryingObject();
            return;
        }
       
        if (Interactable != null)
        {
            Interactable.OnInteract(m_Character);
            m_IsCloseEnoughToInteract = false;
        }           
    }

    public void PickupObject(InteractablePickup pickupObject)
    {
        Debug.Log("Item is being picked up");
        m_PickedUpObject = pickupObject;
        m_PickedUpObject.transform.parent = m_MovementModel.PreviewItemParent;
        m_PickedUpObject.transform.localPosition = Vector3.zero;
        //Hmm so localPosition is supposed to make sure the object is at the parent objects position
        //Altho its the parent im picking up, but it seems to work anyway
        //hmmm...
        m_MovementModel.SetFrozen(true, false, false);
        m_MovementModel.SetIsAbleToAttack(false);
        Helper.SetSortingLayerForAllRenderers(pickupObject.transform, "Characters");
        //Oh now it comes
        Physics2D.IgnoreCollision(m_Collider, pickupObject.GetComponent<Collider2D>());
    }
    public void ThrowCarryingObject()
    {
        m_PickedUpObject.Throw(m_Character);
        m_PickedUpObject = null;
        m_MovementModel.SetFrozen(false, false, false);
        m_MovementModel.SetIsAbleToAttack(true);
    }

    public bool IsCarryingObject()
    {
        return m_PickedUpObject != null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_InteractableCollider.IsTouching(collision))
        {
          //  Debug.Log("Touched this collider");
            if (collision.GetComponent<InteractableBase>()&&collision.GetComponent<InteractableBase>().IsInteractable == true)
            {
                Interactable = collision.GetComponent<InteractableBase>();
                m_IsCloseEnoughToInteract = true;
                interactableNotification.SetIcon();
            }

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(!m_InteractableCollider.IsTouching(collision))
        {
            if (collision.GetComponent<InteractableBase>())
            {
                if (Interactable != null)
                {
                    Interactable.OnStopInteract();
                    Interactable = null;
                    m_IsCloseEnoughToInteract = false;
                    interactableNotification.SetIcon();
                }
            }
        }
    }
}