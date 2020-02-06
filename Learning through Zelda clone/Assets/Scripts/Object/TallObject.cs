using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallObject : MonoBehaviour, IComparable<TallObject>
{
    public SpriteRenderer MySpriteRenderer { get; set; }

    public bool fades;
    private Color defaultColor;
    private Color fadedColor;

    public int CompareTo(TallObject other)
    {
        if(MySpriteRenderer.sortingOrder > other.MySpriteRenderer.sortingOrder)
        {
            return 1;
        }
        else if(MySpriteRenderer.sortingOrder < other.MySpriteRenderer.sortingOrder)
        {
            return -1;
        }
        return 0;
    }

    private void Awake()
    {
        MySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultColor = MySpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.6f;
    }
    public void FadeOut()
    {
        if(fades == true)
        {
            MySpriteRenderer.color = fadedColor;
        }
    }
    public void FadeIn()
    {
        if(fades == true)
        {
            MySpriteRenderer.color = defaultColor;
        }
    }
}
