using UnityEngine;
using System.Collections;

public class CharacterMovementModel : MonoBehaviour {

    public float Speed;
    public float walkingSpeed;
    public float runningSpeed;

    public Vector3 m_MovementDirection;
    public Vector3 m_FacingDirection;
    public Vector2 m_ReceivedDirection; //Ensures that direction can be changed every frame

    private Character m_Character;
    private Rigidbody2D m_Body;

    private bool m_IsFrozen;
    public bool m_IsDirectionFrozen;
    private float m_LastFreezeTime;
    public bool m_IsTakingDamage;
    public bool m_IsRunning;

    public bool m_IsAttacking;
    private bool m_IsAbleToAttack;
    private bool m_IsAbleToSpecialAttack;

  /*  public Transform PreviewItemParent;
    private ItemType m_PickingUpObject = ItemType.None;
    private PickupType m_PickingUpObjectType = PickupType.None;*/

   // private GameObject m_PickupItem;

    public Vector2 m_PushDirection;
    public float m_PushTime;

    public int m_LastSetDirectionFrameCount;

    private bool m_IsOverrideSpeed;
    private float m_OverrideSpeed;

    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Character = GetComponent<Character>();
        m_IsAbleToAttack = true;
        m_IsAbleToSpecialAttack = true;
        walkingSpeed = Speed;
    }
    private void Update()
    {
        UpdatePushTime();
        UpdateDirection();
        ResetReceivedDirection();
    }
    void FixedUpdate()
    {
        UpdateMovement();
    }

    void ResetReceivedDirection()
    {
        m_ReceivedDirection = Vector2.zero;
    }

    virtual public void UpdateDirection()
    {
        //Debug.Log("Facing direction x: " + m_FacingDirection.x + " y: " + m_FacingDirection.y);
        //if (m_FacingDirection.x == 1 && m_FacingDirection.y == -1)
        //{
        //    Debug.Log("Setting to 0");
        //    m_FacingDirection.x = 0;
        //}
        if (m_IsDirectionFrozen == true && m_ReceivedDirection != Vector2.zero)
        {
            return;
        }

        if (m_IsAttacking == true)
        {
            return;
        }

        if (IsBeingPushed() == true)
        {
            m_MovementDirection = m_PushDirection;
            return;
        }

        if (Time.frameCount == m_LastSetDirectionFrameCount)
        {
            return;
        }

        m_MovementDirection = new Vector3(m_ReceivedDirection.x, m_ReceivedDirection.y, 0);

        if (m_ReceivedDirection != Vector2.zero)
        {
            Debug.Log("Setting facing direction");
            Vector3 facingDirection = m_MovementDirection;
            //The problem here is that movement direction does not correspond to facingdirection, since you can move diagonally
            m_FacingDirection = facingDirection;
            if (gameObject.tag == "Player")
            m_LastSetDirectionFrameCount = Time.frameCount;
        }
    }

    void UpdatePushTime()
    {
        m_PushTime = Mathf.MoveTowards(m_PushTime, 0f, Time.deltaTime);
    }

    void UpdateMovement()
    {
        if (m_IsFrozen == true || m_IsAttacking == true)
        {
            m_Body.velocity = Vector2.zero;
            return;
        }
        if (m_MovementDirection != Vector3.zero)
        {
            m_MovementDirection.Normalize();//written to make sure the player doesnt go faster diagonally
        }
        if (IsBeingPushed() == true)
        {
            m_Body.velocity = m_PushDirection;
        }
        else
        {
            float speed = Speed;
            if(m_IsOverrideSpeed == true)
            {
                speed = m_OverrideSpeed;
            }
            m_Body.velocity = m_MovementDirection * Speed;
        }

    }
    public void SetOverrideSpeedEnabled(bool enabled, float speed = 0f)
    {
        m_IsOverrideSpeed = enabled;
        m_OverrideSpeed = speed;
    }

    public bool IsBeingPushed()
    {
        return m_PushTime > 0;
    }

    public bool IsFrozen()
    {
        return m_IsFrozen;
    }

    public float GetTimeSinceFrozen()
    {
        if (IsFrozen() == false)
        {
            return 0f;
        }
        return Time.realtimeSinceStartup - m_LastFreezeTime;
    }

    public void SetFrozen(bool isFrozen, bool isDirectionFrozen, bool affectGameTime)
    {
        m_IsFrozen = isFrozen;
        m_IsDirectionFrozen = isDirectionFrozen;

        if (affectGameTime == true)
        {
            if (isFrozen == true)
            {
                m_LastFreezeTime = Time.realtimeSinceStartup;
                StartCoroutine(FreezeTimeRoutine());
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator FreezeTimeRoutine()
    {
        yield return null;
        Time.timeScale = 0;
    }

    public void SetDirection(Vector2 direction)
    {
        if(direction==Vector2.zero)
        {
           // Debug.Log("Direction was zero");
            return;
        }
        m_ReceivedDirection = direction;
    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public bool IsMoving()
    {
        if (m_IsFrozen == true)
        {
            return false;
        }
        return m_MovementDirection != Vector3.zero;
    }

   /* public void ShowItemPickup(ItemType itemType, PickupType pickupType)
    {
        if (PreviewItemParent == null)
        {
            return;
        }
        ItemData itemData = Database.Item.FindItem(itemType);
        if (itemData == null)
        {
            return;
        }
        SetDirection(new Vector2(0, -1));
        SetFrozen(true, true, true);

        m_PickingUpObject = itemType;
        m_PickingUpObjectType = pickupType;

        m_PickupItem = (GameObject)Instantiate(itemData.Prefab);

        m_PickupItem.transform.parent = PreviewItemParent;
        m_PickupItem.transform.localPosition = Vector2.zero; 
        m_PickupItem.transform.localRotation = Quaternion.identity;

        MonoBehaviour[] components = m_PickupItem.GetComponentsInChildren<MonoBehaviour>();

        foreach(MonoBehaviour component in components)
        {
            component.enabled = false;
            //This disables literally every component in the item
        }
    }*/

 /*   public ItemType GetItemThatIsBeingPickedUp()
    {
        return m_PickingUpObject;
    }*/

    public void PushCharacter(Vector2 pushDirection, float time)
    {
        if(m_IsAttacking == true)
        {
            if (GetComponentInChildren<CharacterAnimationListener>())
            {
                GetComponentInChildren<CharacterAnimationListener>().OnAttackFinished();
            }
            else if (GetComponentInChildren<EnemyAnimationListener>())
            {
                GetComponentInChildren<EnemyAnimationListener>().OnAttackFinished();
            }
        }
        m_PushDirection = pushDirection;
        m_PushTime = time;
    }

 /*   public PickupType GetPickUpType()
    {
        return m_PickingUpObjectType;
    }*/

    public bool CanAttack()
    {
       // Debug.Log("Can attack: " +m_IsAbleToAttack);
        if (m_IsAttacking == true) //This says that, when the animator calls for OnAttackStarted, then the CanAttack returns false. So you can't attack if youre attacking.
            //When the animator calls for OnAttackFinished, then CanAttack returns true.
            //The CanAttack is called by the CharacterMovementBaseControl. If the CanAttack returns false, then MovementBaseControl cannot call "DoAttack" which is a function in THIS script
            //Essentially, CanAttack has full control over whether or not you can attack, as the name implies
        {
            return false;
        }
        if(m_Character != null)
        {
            if (Character.Instance.Equip.m_EquippedWeapon == ItemType.None)
            {
                return false;
            }
        }
        if(IsBeingPushed()==true)
        {
            Debug.Log("Im being pushed!");
            return false;
        }
        if(m_IsTakingDamage == true)
        {
            //is currently set to be used for enemies that are being hurt, because i dont want them to do anything until the animation is over
            return false;
        }
        if(m_IsAbleToAttack == false)
        {
            return false;
        }
        return true;
    }
    public bool CanSpecialAttack()
    {
        //Debug.Log("Can specialattack: " + m_IsAbleToSpecialAttack);
        if (m_IsAttacking == true) //This says that, when the animator calls for OnAttackStarted, then the CanAttack returns false. So you can't attack if youre attacking.
                                   //When the animator calls for OnAttackFinished, then CanAttack returns true.
                                   //The CanAttack is called by the CharacterMovementBaseControl. If the CanAttack returns false, then MovementBaseControl cannot call "DoAttack" which is a function in THIS script
                                   //Essentially, CanAttack has full control over whether or not you can attack, as the name implies
        {
           // Debug.Log("CanSpecialAttack returned false because you're attacking");
            return false;
        }
        if (IsBeingPushed() == true)
        {
         //   Debug.Log("CanSpecialAttack returned false because you're being pushed");
            return false;
        }
        if (m_IsAbleToSpecialAttack == false)
        {
        //    Debug.Log("CanSpecialAttack returned false because the bool is false");
            return false;
        }
        return true;
    }

    public void SetIsAbleToAttack(bool isAbleToAttack)
    {
        m_IsAbleToAttack = isAbleToAttack;
    }

    public void SetIsAbleToSpecialAttack(bool isAbleToSpecialAttack)
    {
        m_IsAbleToSpecialAttack = isAbleToSpecialAttack;
    }

    public void DoAttack()
    {
       // Debug.Log("Attacked");
    }

    public void DoSpecialAttack(float time)
    {
       // Debug.Log("Did a special attack");
        StartCoroutine(SpecialAttackWaitTime(time));
    }
    IEnumerator SpecialAttackWaitTime(float time)
    {
        SetIsAbleToSpecialAttack(false);
        m_IsAttacking = true;
        yield return new WaitForSeconds(time);
        SetIsAbleToSpecialAttack(true);
        m_IsAttacking = false;
    }
    public void OnAttackStarted()//Called by CharacterAnimationListener
    {
        m_IsAttacking = true;
        //m_IsAttacking is a part of THIS script. Check public book CanAttack
      //  Debug.Log("OnAttackStarted");
    }

    public void OnAttackFinished()
    {
        m_IsAttacking = false;
     //   Debug.Log("OnAttackFinished");
    }

    public float GetCastTime(float castTime)
    {
        float newCastTime = castTime * 1.2f;
      //  Debug.Log(newCastTime);
        return newCastTime; //this is hardcoded for now
    }
}
