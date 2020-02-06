using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGamePadControl : CharacterMovementBaseControl {

    void Start()
    {
       
    }

    void Update()
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
    }

    void UpdateAttack() //Do not attempt to make any combo script here. Do it in the OnAttackPressed
    {
        if (Input.GetButtonDown("Attack"))
            OnAttackPressed(); //OnAttackPressed can be found in the CharacterMovementBaseControl
    }

    void UpdateAction()
    {
        if (Input.GetButtonDown("Interact"))
            OnActionPressed();
    }

    void UpdateDirection()
    {
        Vector2 newDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
        float threshold = 0.4f;
        if (Mathf.Abs(newDirection.x)<threshold)
        {
            newDirection.x = 0;
        }
        else
        {
            newDirection.x = Mathf.Sign(newDirection.x);
        }
        if(Mathf.Abs(newDirection.y)<threshold) // abs ignores sign
        {
            newDirection.y = 0;
        }
        else
        {
            newDirection.y = Mathf.Sign(newDirection.y);
        }
   
            
            /*newDirection.x = Mathf.Sign(newDirection.x);
            /*
             Mathf is a collection of common math functions
             Mathf.Sign apparently returns the sign of f
             Definition is
             public static float Sign(float f);
             According to tutorial, the Mathf.Sign returns +1 if the value is positive
             and -1 if value is negative
             */
        
        /*if(newDirection.y != 0)
        {
            //newDirection.y = Mathf.Sign(newDirection.y);
        }*/

        SetDirection(newDirection);

    }
}
