using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIdentifier : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float castTime;

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public float MyCastTime
    {
        get
        {
            return castTime;
        }
    }
}
