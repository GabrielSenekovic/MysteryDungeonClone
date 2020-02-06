using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public GameObject vineTip;
    public GameObject player;
    public GameObject targetObject;
    private Vector2 targetLocation;
    public SpriteRenderer vineLength;
    public bool expanding = false;

    private float vineLengthSize = 0;
    private bool directionSet = false;

    public void Update()
    {
        if(vineTip.GetComponent<VineTip>().CheckIfCollided())
        {
            if(expanding == true){expanding = false;}
        }
        if(vineTip.GetComponent<VineTip>().CheckLastCollided()!=null)
        {
            if(vineTip.GetComponent<VineTip>().CheckLastCollided().GetComponent<GrabableBase>())
            {
                targetObject = vineTip.GetComponent<VineTip>().CheckLastCollided();
            }
        }
        if(expanding == true && vineLengthSize != 0)
        {
            if(vineLength.size.y < vineLengthSize - 0.25)
            {
                if(directionSet == false)
                {
                    vineTip.GetComponent<SpriteRenderer>().enabled = true;
                    SetVineDirection();
                }
                ExpandVine();
            }
            else
            {
                expanding = false;
            }
        }
        else if(expanding == false && vineLength.size.y > 0)
        {
            vineTip.GetComponent<Collider2D>().enabled = false;
            if (targetObject == null)
            {
                ShrinkVine();
            }
            else
            {
                if(targetLocation == Vector2.zero)
                {
                    targetLocation = vineTip.transform.position;
                }
                PullPlayer();
            }
        }
        else
        {
            vineTip.GetComponent<SpriteRenderer>().enabled = false;
            vineTip.GetComponent<VineTip>().Reset();
            targetObject = null;
            directionSet = false;
            targetLocation = Vector2.zero;
            vineTip.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void SetVineSize(float size)
    {
        vineLengthSize = size;
    }

    public void SetVineDirection()
    {
        Vector3 direction = transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        if (direction.x == 1 && direction.y == 0)
        {
            //east
            vineLength.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
            vineTip.gameObject.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            //west
            vineLength.gameObject.transform.eulerAngles = new Vector3(0, 0, 270);
            vineTip.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            //north
            Debug.Log("Shooting north");
            vineLength.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
            vineTip.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x == 0 && direction.y == -1)
        {
            vineLength.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            vineTip.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            Debug.Log("Vine is CONFUSION");
        }
        directionSet = true;
    }

    public void ExpandVine()
    {
        Vector3 targetPosition = transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        if(targetPosition.x == 0 && targetPosition.y ==1)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x, player.transform.position.y + vineLength.size.y+1),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == 0 && targetPosition.y == -1)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x, player.transform.position.y - vineLength.size.y-1),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == 1 && targetPosition.y == 0)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x + vineLength.size.y+1, player.transform.position.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == -1 && targetPosition.y == 0)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x - vineLength.size.y - 1, player.transform.position.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }

        vineLength.size = Vector2.Lerp(vineLength.size, new Vector2(0.25f, vineLengthSize), 5 * Time.deltaTime);
    }
    public void ShrinkVine()
    {
        Vector3 targetPosition = transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        if (targetPosition.x == 0 && targetPosition.y == 1)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x, player.transform.position.y + vineLength.size.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == 0 && targetPosition.y == -1)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x, player.transform.position.y - vineLength.size.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == 1 && targetPosition.y == 0)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x + vineLength.size.y, player.transform.position.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }
        else if (targetPosition.x == -1 && targetPosition.y == 0)
        {
            vineTip.transform.position = Vector2.Lerp(
            new Vector2(player.transform.position.x - vineLength.size.y, player.transform.position.y),
            player.transform.position
            , 5 * Time.deltaTime);
        }

        vineLength.size = Vector2.Lerp(vineLength.size, new Vector2(0.25f, 0), 5 * Time.deltaTime);
        if(vineLength.size.y < 0.25)
        {
            vineLength.size = new Vector2(0.25f, 0); 
        }
    }
	public void PullPlayer()
    {
        Vector2 direction = transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
        Debug.Log("Now pulling player!");
        vineTip.transform.position = targetLocation;
        vineLength.size = new Vector2(0.25f ,Vector2.Distance(vineTip.transform.position, player.transform.position));
        if(Vector2.Distance(targetObject.transform.position, player.transform.position) < 1)
        {
            vineLength.size = new Vector2(0.25f, 0);
        }
        if(vineTip.transform.position.x > player.transform.position.x && direction.y == 0)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20);
        }
        else if (vineTip.transform.position.x < player.transform.position.x && direction.y == 0)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20);
        }
        if (vineTip.transform.position.y > player.transform.position.y && direction.x == 0)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20);
        }
        else if (vineTip.transform.position.y < player.transform.position.y && direction.x == 0)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 20);
        }
    }
}
