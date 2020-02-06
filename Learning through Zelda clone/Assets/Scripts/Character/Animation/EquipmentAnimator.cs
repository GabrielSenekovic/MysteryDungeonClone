using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAnimator : MonoBehaviour
{
    public CharacterAnimationListener AnimationListener;
    public SpriteRenderer Renderer;
    public EquipmentAnimationSpriteList SpriteList;
    public enum AnimationType
    {
        Hair,
        
        Sword,
        Bow,
    }

    public AnimationType MyAnimationType;

    private void Awake()
    {
        AnimationListener = transform.parent.transform.parent.GetComponent<CharacterAnimationListener>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        SpriteList = GetComponentInChildren<EquipmentAnimationSpriteList>();
        if (AnimationListener == null)
        {
            Debug.LogWarning("Can't find an animationListener!");
        }
        else
        {
            switch(MyAnimationType)
            {
                case AnimationType.Hair: AnimationListener.Hair = this;
                    break;
                case AnimationType.Sword:
                    break;
                case AnimationType.Bow:
                    break;
            }
        }
    }
    public void SetSprite(string sprite)
    {
        switch(sprite)
        {
            default: Debug.LogWarning("There is no sprite with this name");
                break;
            case "IdleNorth":
                Renderer.sprite = SpriteList.IdleNorth;
                break;
            case "IdleSouth":
                Renderer.sprite = SpriteList.IdleSouth;
                break;
            case "IdleEast":
                Renderer.sprite = SpriteList.IdleEast;
                break;
            case "IdleWest":
                Renderer.sprite = SpriteList.IdleWest;
                break;
            case "WalkingNorth":
                break;
            case "WalkingSouth":
                break;
            case "WalkingEast":
                break;
            case "WalkingWest":
                break;
        }
    }

}
