using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

interface IUseable
//This is required for everything you can click on
{
    Sprite MyIcon
    {
        get;
    }

    void Use();
}
