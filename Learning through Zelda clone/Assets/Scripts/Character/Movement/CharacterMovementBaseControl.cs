using UnityEngine;
using System.Collections;

public class CharacterMovementBaseControl : MonoBehaviour {
    //All functions in this script is called from the CharacterMovemementKeyboardControl
    protected CharacterMovementModel m_MovementModel;
    private CharacterInteractionModel m_InteractionModel;
    private CharacterMovementView m_MovementView;
    private AttackManager m_AttackManager;
    public Inventory m_Inventory; //this is for debugging purposes

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_MovementView = GetComponent<CharacterMovementView>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
        if(gameObject.tag == "Player")
        {
            m_AttackManager = GetComponentInChildren<AttackManager>();
        }
    }

    protected void SetDirection(Vector2 direction)
    {
        if (m_MovementModel == null)
        {
            //this function is called by the player controller when you press the move buttons
            Debug.Log("Movement model was null");
            return;
        }

        m_MovementModel.SetDirection(direction);
    }

    protected void OnActionPressed()
    {
        if (m_InteractionModel == null)
        {
            return;
        }
       m_InteractionModel.OnInteract();
    }
    protected void OnAttackPressed() //This can be called from the CharacterMovementKeyboardControl
        //Do combos within the animations so in the MovementView
    {
        if (m_MovementModel == null)
        {
            return;
        }

        if (m_MovementModel.CanAttack() == false) //This check makes sure that you can attack. According to the MovementModel, you cannot attack if you are already attacking. Whether you are attacking or not is decided by the animator.
        {
            return; //remember that the return takes the code out of the OnAttackPressed() and that the following calls wont occur if then, the canattack is false.
        }

        m_MovementModel.DoAttack(); //Which will do no more than send a debug message atm
        m_MovementView.DoAttack(); //Which should trigger the animation. I think its in the animations you should do the combos

    }
    protected void OnSpecialAttackPressed(int attackIndex)
    {
        if (m_MovementModel == null)
        {
            Debug.Log("Cant do a special attack because there is no movement model");
            return;
        }
        if (m_MovementModel.CanSpecialAttack() == false)
        {
            Debug.Log("Couldn't do a special attack because CanSpecialAttack was false");
            return; //Some spells should be able to override this
        }
        m_AttackManager.OnAttack(attackIndex);
        m_MovementModel.DoSpecialAttack(m_AttackManager.GetStillTime(attackIndex));
    }
    protected void OnRun()
    {
        m_MovementModel.Speed = m_MovementModel.runningSpeed;
        m_MovementModel.m_IsRunning = true;
        m_MovementView.OnRun();
    }
    protected void OnStopRunning()
    {
        m_MovementModel.Speed = m_MovementModel.walkingSpeed;
        m_MovementModel.m_IsRunning = false;
        m_MovementView.OnStopRunning();
    }
}
