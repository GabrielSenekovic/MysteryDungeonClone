﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public interface IClickable
{
    Image MyIcon
    {
        get;
        set;
    }

    int MyCount
    {
        get;
    }

    Text MyStackText
    {
        get;
    }
}
