using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonalPath : MonoBehaviour {
    public float timeToGoThisDirection;
    public float Speed;
    public bool IsMoving;
    private void Start()
    {
        IsMoving = true;
    }
    void Update()
    {
        if (IsMoving == true)
        {
            StartCoroutine(Move());
        }
    }
    IEnumerator Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (IsMoving == true)
        {
            rb.velocity = transform.right * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = (transform.right + transform.up) * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = transform.up * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = (transform.up -transform.right) * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = -transform.right * Speed; //go left
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = (-transform.right - transform.up) * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = -transform.up * Speed; //go down
            yield return new WaitForSeconds(timeToGoThisDirection);
            rb.velocity = (-transform.up + transform.right) * Speed;
            yield return new WaitForSeconds(timeToGoThisDirection);
        }

    }
}
