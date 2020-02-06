using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderImitator : MonoBehaviour {

    public BoxCollider2D ParentBoxCollider;
    public BoxCollider2D AnimatedBoxCollider;
    private void Update()
    {
        ParentBoxCollider.size = AnimatedBoxCollider.size;
        ParentBoxCollider.offset = AnimatedBoxCollider.offset;
    }
}
