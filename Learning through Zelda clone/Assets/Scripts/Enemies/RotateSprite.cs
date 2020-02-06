using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour {
    public bool IsRotating;
    public float rotationAngle;
	void Update ()
    {
		if(IsRotating == true)
        {
            SpriteRenderer rotatingSprite = GetComponent<SpriteRenderer>();
            rotatingSprite.transform.Rotate(0, 0, rotationAngle * Time.deltaTime);
        }
	}
}
