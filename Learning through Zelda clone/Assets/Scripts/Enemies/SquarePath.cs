using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePath : MonoBehaviour {
    public float timeToGoThisDirection;
    public float Speed;
    public bool IsMoving;
    private void Start()
    {
        IsMoving = true;
        if(IsMoving==true)
            {
            //Debug.Log(gameObject.name + " is now moving in a square");
            StartCoroutine(Move());
            }
    }
    IEnumerator Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (IsMoving==true)
        {
            GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.velocity = transform.right * Speed;
        yield return new WaitForSeconds(timeToGoThisDirection);
            GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = transform.up * Speed;
        yield return new WaitForSeconds(timeToGoThisDirection);
            GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.velocity = -transform.right * Speed; //go left
        yield return new WaitForSeconds(timeToGoThisDirection);
            GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = -transform.up * Speed; //go down
        yield return new WaitForSeconds(timeToGoThisDirection);
        }
        
    }
}
