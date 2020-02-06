using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    // This script has to make the bow spawn an arrow and then release it at a certain point
    private void Awake()
    {
        //spawn an arrow prefab
    }
    public void ReleaseArrow()
    {
        //Will erase the arrow from this bow and spawn a projectile prefab
        /*
         * This means the arrow has to have a bool like, isbeingshot
         * so it can be false when its in the bow arrowholder
         * and it can be true when its being spawned
         * this function will be called by the bowanimationlistener
         */
    }
}
