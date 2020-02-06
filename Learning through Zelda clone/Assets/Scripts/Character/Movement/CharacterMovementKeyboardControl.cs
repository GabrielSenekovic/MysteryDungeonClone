using UnityEngine;
using System.Collections;

public class CharacterMovementKeyboardControl : CharacterMovementBaseControl {

    // This script tells you the controls. It says what functions will be called when the buttons are pressed.
    //You can find each function, such as OnAttackPressed and OnActionPressed, in the CharacterMovementBaseControl
    void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    void Update () {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
        UpdateSpecialAttack();
        UpdateSpeed();
	}

    void UpdateAttack() //Do not attempt to make any combo script here. Do it in the OnAttackPressed
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            OnAttackPressed(); //OnAttackPressed can be found in the CharacterMovementBaseControl
        }
    }

    void UpdateSpecialAttack()
    {
        //this is temporary. When the skill screen is available, attacks will be put with these attacks as well
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("'U' has been pressed");
            OnSpecialAttackPressed(0);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("'I' has been pressed");
            OnSpecialAttackPressed(1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("'O' has been pressed");
            OnSpecialAttackPressed(2);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("'P' has been pressed");
            OnSpecialAttackPressed(3);
        }

    }

    void UpdateAction()
    {
        //This will control the dialogmanager if the dialogbox is visible!
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!DialogBox.IsVisible())
            {
                OnActionPressed();
            }
            else
            {
                DialogManager.DoSpeak();
            }
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            newDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);

    }
    void UpdateSpeed()
    {
        //Change the button because this is already the interaction button. Or change it around idk
        if(Input.GetKey(KeyCode.Space))
        {
            OnRun();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            OnStopRunning();
        }
    }
}
