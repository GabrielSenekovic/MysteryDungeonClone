using UnityEngine;
using System.Collections;

public class CharacterMovementView : MonoBehaviour {
    //The movementview is going to set all the different variables to the animator
    public enum ShieldDirection
    {
        Front,
        Right,
        Left,
        Back,
        FrontHalf,
        BackHalf,
    }
    public Animator Animator;
    public Transform WeaponParent;
    public GameObject[] spellPrefab;

    public CharacterMovementModel m_MovementModel;
    public CharacterEquipModel m_EquipModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_EquipModel = GetComponent<CharacterEquipModel>();

        if (Animator == null)
        {
            Debug.LogError("Character animator not setup!");
            enabled = false;
        }
    }

    // Use this for initialization
   /* void Start() {
        SetItemActive(m_EquipModel.WeaponParent, false);
    }*/

    // Update is called once per frame
    virtual public void Update()
    {
      //  ShowShield();
        UpdateDirection();
      //  UpdatePickingUpAnimation();
        UpdateHurt();
       // UpdateShield();
    }

  /*  void UpdateShield()
    {
        Animator.SetBool("HasShield", m_EquipModel.GetEquippedShield() != ItemType.None);
    }*/

  /*  void UpdatePickingUpAnimation()
    {
        bool isPickingUpTwoHanded = false;
        bool isPickingUpOneHanded = false;

        if (m_MovementModel.IsFrozen() == true)
        {
            ItemType pickupItem = m_MovementModel.GetItemThatIsBeingPickedUp();
            if (m_MovementModel.GetItemThatIsBeingPickedUp() != ItemType.None)
            {
                ItemData itemData = Database.Item.FindItem(pickupItem);
                switch (itemData.Animation)
                {
                    case ItemData.PickupAnimation.OneHanded:
                        isPickingUpOneHanded = true;
                        break;
                    case ItemData.PickupAnimation.TwoHanded:
                        isPickingUpTwoHanded = true;
                        break;
                }
            }
        }
        Animator.SetBool("IsPickingUpTwoHanded", isPickingUpTwoHanded);
        Animator.SetBool("IsPickingUpOneHanded", isPickingUpOneHanded);
    }*/

    public void UpdateHurt()
    {
        Animator.SetBool("IsHurt", m_MovementModel.IsBeingPushed()); // okay so later you can set IsHurt to another function, because the character is being pushed anyway
    }

    virtual public void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetFacingDirection();

        if (direction != Vector3.zero)
        {
            if(direction.x != 1 || direction.y != 1)
            {
            Animator.SetFloat("DirectionX", direction.x);
            Animator.SetFloat("DirectionY", direction.y);
            }
            
        }
        if(m_MovementModel.m_IsRunning == false)
        {
            Animator.SetBool("IsMoving", m_MovementModel.IsMoving());
        }
    }

    public void DoAttack()
    {
        /*
        Here is the hierarchy
        MovementKeyboardControl: Presses X, directs to the basecontrols "OnAttackPressed"
        OnAttackPressed will check if you can attack
        Then it will direct this info to DoAttack of the MovementModel and the MovementView
        This is the MovementView, which decides EVERYTHING about the attack, currently
        The MovementModel also contains the functions of OnAttackStarted and OnAttackFinished, that both control the m_IsAttacking bool
        I make the animator call on these functions, to turn off and on the bool
        */
        /*
        As for combos. Should I not be able to make a bunch of if statements in here? Because then, if the condition of the if statement is true
        wouldnt the code trigger THAT if statement instead of the rest? Like if you ended the if statement with return; to bring it out of the function.
        Then the last combo attack would be highest up in the DoAttack function and the first furthest down

        The only possible obstacle i could think of, is that the CanAttack function will prevent the DoAttack from running.
        I also cant make the function on the animation shorter than the animation, because then the sword will disappear (Visuals turn off at OnAttackFinished)
        Therefore I have to make it so that the function that makes the sword go away and the function that makes it possible to attack again are two different functions
        Essentially, I need to give the CharacterAnimationLister THREE functions!
        One function at the beginning will tell the basecontrol you cant attack and turn on the visuals.
        One function next to last will tell the basecontrol you can attack.
        The last function will tell the movement view to turn off the visuals.
        */
        //if(WeaponParent.)

        WeaponType equippedWeaponType = m_EquipModel.GetEquippedWeaponType();
        if(equippedWeaponType == WeaponType.None)
        {
            Debug.Log("This item has no WeaponType. It is no weapon.");
            return;
        }
        if(equippedWeaponType == WeaponType.Sword)
        {
            Animator.SetTrigger("SwordIsAttacking");
        }
        if(equippedWeaponType == WeaponType.Gun)
        {
            Animator.SetTrigger("GunIsAttacking");
        }
        if(equippedWeaponType == WeaponType.Bow)
        {
            Animator.SetTrigger("BowIsAttacking");
        }
    }
    public void OnAttackStarted() //Called by CharacterAnimationListener
    {
        //SetWeaponActive(true);
    }
    public void OnAttackFinished()
    {
        //SetWeaponActive(false);
    }
 /*   public void ShowWeapon()
    {
        SetItemActive(m_EquipModel.WeaponParent, true);
    }
    public void HideWeapon()
    {
        SetItemActive(m_EquipModel.WeaponParent, false);
    }
    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        SetSortingOrderOfItem(m_EquipModel.WeaponParent, sortingOrder);
    }
    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        SetSortingOrderOfItem(m_MovementModel.PreviewItemParent, sortingOrder);
    }
    public void ShowShield()
    {
        SetItemActive(m_EquipModel.ShieldParent, true);
    }
    public void HideShield()
    {
        SetItemActive(m_EquipModel.ShieldParent, false);
    }

    public void SetSortingOrderOfShield(int sortingOrder)
    {
        SetSortingOrderOfItem(m_EquipModel.ShieldParent, sortingOrder);
    }
    public void ForceShieldDirection(ShieldDirection direction)
    {
        //This is called by the CharacterAnimationListener.SetShieldDirection
        ShieldView shield = m_EquipModel.ShieldParent.GetComponentInChildren<ShieldView>();
        if (shield == null)
        {
            return;
        }
        shield.ForceShieldDirection(direction);
    }
    public void ReleaseShieldDirection()
    {
        ShieldView shield = m_EquipModel.ShieldParent.GetComponentInChildren <ShieldView>();
        if(shield==null)
        {
            return;
        }
        shield.ReleaseShieldDirection();
    }*/
    public void SetSortingOrderOfItem(Transform itemParent, int sortingOrder)
    {
        if(itemParent == null)
        {
            return;
        }
        SpriteRenderer[] spriteRenderers = itemParent.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }
    public void SetItemActive(Transform itemParent, bool doActivate)
    {
        if(itemParent == null)
        {
            return;
        }
        itemParent.gameObject.SetActive(doActivate);
    }
    /*void SetWeaponActive(bool doActivate)
    {
        if(WeaponParent==null)
        {
            return;
        }

        WeaponParent.gameObject.SetActive(doActivate);
        /*Debug.Log("SetWeaponActive:" + WeaponParent.childCount);
        for (int i = 0; i < WeaponParent.childCount; ++i)
            /*
             A for loop has three parts. for (initializer;condition;iterator)
             The initializer is excecuted only once, before the loop
             i is a common arbitrary loop variable
             The next part must be true, otherwise the loop wont be initialized
             So the loop starts only if the amount of children of the weaponparent is more than 0
             Then the iterant adds 1 to i every time the loop is called
             So this loop can ONLY be called every time you get a new item, because only THEN is the childcount larger than i
             And since the loop ends with 1 being added to i, the foor loop is only called once everytime an item is picked
             
        {
            WeaponParent.GetChild(i).gameObject.SetActive(doActivate); //This was complicated. 58:48 at livestream week 5
        } */
    public void DoSpecialAttack()
    {
        //First it should get what the special attack IS
        //If its a spell, for example
        CharacterSpellCaster m_Spellcaster = GetComponent<CharacterSpellCaster>();
        //Make it so that theres a prefab that is set by a menu, and this prefab is the one that will get spawned with onspawn
        //The prefab and all its data is gathered from a database of spells
        //So here, it will get from the menu or something, what is the equipped prefab, and then, it will send it to OnSpawn
        //Make it also, so that there are 4 prefabs
        //Hold the O button, to change the U, I, J, K buttons into spell buttons
        m_Spellcaster.OnSpawn();
    }

    public void SetBoolFalse(string boolName, AttackIdentifier attack)
    {
        StartCoroutine(SetBoolFalseRoutine(boolName, GetComponent<CharacterMovementModel>().GetCastTime(attack.MyCastTime)));
    }

    public void OnRun()
    {
        Animator.SetBool("IsRunning", true);
    }
    public void OnStopRunning()
    {
        Animator.SetBool("IsRunning", false);
    }

    public IEnumerator SetBoolFalseRoutine(string boolName, float CastTime)
    {
        Debug.Log("setting bool false");
        yield return new WaitForSecondsRealtime(GetComponent<CharacterMovementModel>().GetCastTime(CastTime));
        Animator.SetBool(boolName, false);
    }
}
