using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLastPosition : MonoBehaviour {

    public GameObject followThis;
    public float FollowLag;
    public float Speed;


    private void Update()
    {
        StartCoroutine(goToLastPosition());
    }
    IEnumerator goToLastPosition()
    {
        Vector2 targetPosition = followThis.transform.position;
        yield return new WaitForSeconds(FollowLag);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * Speed);
    }
}
