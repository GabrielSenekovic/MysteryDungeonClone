using UnityEngine;
using System.Collections;

public class CharacterAnimationListener : MonoBehaviour
{

    /*
    This is a script that creates functions that can be placed on the animations
    However
    For functions to be used on the animator, they have be on the same gameobject as the animator is
    The animator cannot call functions from a parent object
    However, the parent objects are controlling the animator
    So the parent objects need to know the information about functions placed on the animation
    This script creates these functions, then tells the parent object what functions to call within its own scripts
    I think this is the only script that can get information from such functions in the animator
    So for example, if youre within a timeframe that enables a combo
    */
    public CharacterAdvancedMovementModel MovementModel;
    public CharacterAdvancedMovementView MovementView;
    public EquipmentAnimator Hair;

    //Have a variable that is the item script, it will change the sprite of it. Have the item set it on its own.


    public void OnAttackStarted(AnimationEvent animationEvent)
    /*So this OnAttackStarted is a function called by the animator! I can put this, and OnAttackFinished as function on a frame of an animation, and that frame will change this bool! This way, a bool
    can be true only during a duration of the animation!
    I repeat, this function is the one called by the animator. This then calls the same OnAttackStarted in the MovementView and the MovementModel.
    It gets information from the animator on its gameobjects
    and gives it to a parent
    */
    {
        if (MovementModel != null)
        {
            MovementModel.OnAttackStarted(); //This says to do the OnAttackStarted if the movementmodel returns null
            //This will make the MovementModel know when you are attacking or not, so it can decide whether you can attack or not
        }
        if (MovementView != null)
        {
            MovementView.OnAttackStarted(); //This will turn on and off the visuals of the weapon
        }
        ShowWeapon();
        SetSortingOrderOfWeapon(animationEvent.intParameter);
        SetShieldDirection(animationEvent.stringParameter);
    }

    public void OnAttackFinished()
    {
        if (MovementModel != null)
        {
            MovementModel.OnAttackFinished(); //I need to separate these two. Into OnAttackFinished1 and OnAttackFinished2
        }
        if (MovementView != null)
        {
            MovementView.OnAttackFinished();
        }
    }
    public void ShowWeapon()
    {
        if (MovementView != null)
        {
            MovementView.ShowWeapon();
        }
    }
    public void HideWeapon()
    {
        if (MovementView != null)
        {
            MovementView.HideWeapon();
        }
    }
    public void ShowShield()
    {
        if (MovementView != null)
        {
            MovementView.ShowShield();
        }
    }
    public void HideShield()
    {
        if (MovementView != null)
        {
            MovementView.HideShield();
        }
    }

    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        if (MovementView != null)
        {
            MovementView.SetSortingOrderOfWeapon(sortingOrder);
        }
    }
    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        if (MovementView != null)
        {
            MovementView.SetSortingOrderOfPickupItem(sortingOrder);
        }
    }

    public void SetShieldDirection(string direction)
    {
        if (MovementView == null || direction == "")
        {
            return;
        }

        switch (direction)
        {
            default:
                Debug.LogWarning("Shield direction '" + direction + "' does not exist.");
                break;
            case "Front":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Front);
                break;
            case "Back":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Back);
                break;
            case "Left":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Left);
                break;
            case "Right":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Right);
                break;
        }
    }
    public void SetAnimation(string sprite)
    { 
        if(Hair != null)
        {
            Hair.SetSprite(sprite);
        }
    }
    public void UnFreeze()
    {
        if(MovementModel != null)
        {
            MovementModel.SetFrozen(false, false, false);
        }
        else
        {
            Debug.Log("Movementmodel is null");
        }
    }
}
