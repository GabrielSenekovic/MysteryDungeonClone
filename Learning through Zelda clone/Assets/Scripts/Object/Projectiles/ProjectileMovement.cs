using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {
    public Vector3 startingPosition;
    public Vector3 targetDirection;

    public float Speed;
    public float SavedSpeed;

    public float lifeSpan;
    public bool Spawning;
    public bool MovingOnSpawn;
    public Animator Animator;

    public bool SlowingDown;
    public float slowDownTime;
    public float slowDownSpeed;

    public bool Homing;

    public bool Sucking;
    public float suckSpeed;

    public bool Trapping;

    private Rigidbody2D myRigidbody;
    private Transform target;

    public GameObject caster;
    public AttackIdentifier attackIdentifier;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Sucking == true)
        {
            GameObject playerObject = FindObjectOfType<CharacterAdvancedMovementModel>().gameObject;
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            if (distance <= 5)
            {
                playerObject.transform.position = Vector2.MoveTowards(playerObject.transform.position, transform.position, suckSpeed * Time.deltaTime / distance);
            }
        }
        if (Trapping == true)
        {
            GameObject playerObject = FindObjectOfType<CharacterAdvancedMovementModel>().gameObject;
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            playerObject.transform.position = Vector2.MoveTowards(playerObject.transform.position, gameObject.transform.position, suckSpeed * distance * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() //Oliver explains that gizmos helps you create scripts that can only be seen in the editor
    {
        Gizmos.color = Color.red; //shows room outline. Always the size of the room
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0));
    }

    public void OnSpawn()
    {
        // Debug.Log("OnSpawn has been called with: " + startingPosition + " and " + targetDirection);

        if (Spawning == true)
        {
            SavedSpeed = Speed;
            Speed = 0;
            return;
        }
        if (Homing == true)
        {
            if (target != null)
            {
                targetDirection = target.position - transform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; //Gives you the angle in degrees
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //This rotates the projectile to face the target
            }
        }
        if (MovingOnSpawn != true)
        {
            float waitTime;
            if (caster != null)
            {
            //    Debug.Log(attackIdentifier.MyCastTime);
                waitTime = caster.transform.parent.GetComponentInParent<CharacterMovementModel>().GetCastTime(attackIdentifier.MyCastTime);
            }
            else
            {
                waitTime = 1;
            }
            StartCoroutine(WaitOnSpawn(waitTime));
        }
        else
        {
            myRigidbody.velocity = targetDirection.normalized * Speed;
            StartCoroutine(DestroyProjectile());
        }
        if (SlowingDown == true)
        {
            StartCoroutine(WaitUntilSlowDown(slowDownTime));
        }
        //transform.TransformDirection(targetDirection) * Speed;
        /*
         * So if TargetDirection is 1,0,0
         * then this is just rb.velocity = transform.right
          */
    }
    public void Move()
    {
        //Debug.Log("Move is being called");
        myRigidbody.velocity = targetDirection.normalized * Speed;
        StartCoroutine(DestroyProjectile());
    }
    public void OnSlowDown()
    {
       // Debug.Log("Now slowing down");
        myRigidbody.velocity = targetDirection.normalized * Mathf.Lerp(Speed, 0f, Time.deltaTime * slowDownSpeed);
        if (myRigidbody.velocity.x <= 0.1f & myRigidbody.velocity.y <= 0.1f)
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
    IEnumerator WaitOnSpawn(float waitTime)
    {
     //   Debug.Log("WaitOnSpawn commenced");
        yield return new WaitForSecondsRealtime(waitTime);
        Move();
    }
    IEnumerator WaitUntilSlowDown(float slowDownTime)
    {
      //  Debug.Log("Waiting for slowing down");
        yield return new WaitForSecondsRealtime(slowDownTime);
        OnSlowDown();
    }
    public void OnShoot()
    {
        Speed = SavedSpeed;
        Spawning = false;
        OnSpawn();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
      //  Debug.Log("Casters tag is: " + caster.tag);
        if(caster.tag == "Player" && collider.tag == "Enemy")
        {
           // Debug.Log("Casters tag is player");
            AttackableBase attackedTarget = collider.gameObject.GetComponent<AttackableBase>();
            if (attackedTarget != null)
            {
             //   attackedTarget.OnHit(GetComponentInChildren<CircleCollider2D>(), ItemType.Sword);
                Destroy(gameObject);
            }
        }
        if(caster.tag == "Enemy" && collider.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(caster.tag == "Player" && collider.tag == "Player")
        {
           // Debug.Log("You hit yourself, idiot");
        }
    }
}
