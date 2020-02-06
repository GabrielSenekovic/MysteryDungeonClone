using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwiftControl : CharacterEnemyGeneralControl
{
    [SerializeField]
    private AttackManager closeRangeAttack;
    [SerializeField]
    private AttackManager middleRangeAttack;
    [SerializeField]
    private AttackManager longRangeAttack;
    [SerializeField]
    private AttackManager specialAttack;
    [SerializeField]
    private AttackIdentifier barrier;
    [SerializeField]
    private AttackIdentifier transformation;

    public Dialog startDialog;

    bool Attack = true;

    bool ActivateBoss = false;

    public System.Random randomNumber = new System.Random();

    override public void Update()
    {
        if(ActivateBoss == false)
        {
            if(MyCamera.Instance.CurrentRoom == myRoom && MyCamera.Instance.m_IsUpdatingPosition == false)
            {
                if(InitiatedDialog == false)
                {
                    DialogManager.OnSpeak(startDialog);
                    InitiatedDialog = true;
                }
                if(!DialogBox.IsVisible())
                {
                    AudioManager.ChangeBGM(RoomManager.Instance.bossTheme);
                    ActivateBoss = true;
                }
            }
        }
        if(ActivateBoss == true)
        {
            if(UIBossHealthBar.MyInstance.HealthModel == null)
            {
                UIBossHealthBar.MyInstance.HealthModel = GetComponentInChildren<AttackableEnemy>();
                UIBossHealthBar.MyInstance.RevealImage();
            }
            if (!m_MovementModel.CanAttack()) //this likely wont be here later on
            {
                return;
            }
            UpdateDirection();
            if (m_IsCharacterInRange != null)
            {
                Vector2 myPosition = new Vector2(gameObject.transform.position.x + MyBodyCollision.GetComponent<BoxCollider2D>().offset.x, gameObject.transform.position.y + MyBodyCollision.GetComponent<BoxCollider2D>().offset.y);
                Vector2 otherPosition = new Vector2(m_IsCharacterInRange.transform.position.x + m_IsCharacterInRange.GetComponent<BoxCollider2D>().offset.x, m_IsCharacterInRange.transform.position.y + m_IsCharacterInRange.GetComponent<BoxCollider2D>().offset.y);
                // Debug.Log(Vector2.Distance(otherPosition, myPosition));
                Vector3 direction = m_MovementModel.GetFacingDirection();
                if (direction.x == 0 & direction.y == 0)
                {
                    return; //this makes it so that the character cant shoot something without moving first
                }
                if (Vector2.Distance(otherPosition, myPosition) <= 1.3)
                {
                    if (Attack == true)
                    {
                        closeRangeAttack.OnAttackByEnemy();
                        Attack = false;
                        StartCoroutine(WaitUntilAbleToAttack(1));
                    }
                }
                if (Vector2.Distance(otherPosition, myPosition) >= 1.3 & Vector2.Distance(otherPosition, myPosition) <= 3)
                {
                    if (Attack == true)
                    {
                    }
                }
                if (Vector2.Distance(otherPosition, myPosition) >= 3)
                {
                    if (Attack == true)
                    {
                        Debug.Log(gameObject.name + " is making a long range attack");
                        longRangeAttack.OnAttackByEnemy();
                        Attack = false;
                        StartCoroutine(WaitUntilAbleToAttack(2));
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(gameObject.transform.position.x + MyBodyCollision.GetComponent<BoxCollider2D>().offset.x, gameObject.transform.position.y + MyBodyCollision.GetComponent<BoxCollider2D>().offset.y), new Vector3(1, 1, 0));
    }

    void UpdateDirection()
    {
        if(!m_IsCharacterInRange)
        {
            Debug.Log("There is no character in range so " + gameObject +  " can not move");
            return;
        }
        Vector2 direction = Vector2.zero; //this seems to declare the vector AND put it at zero, somehow
        direction = m_IsCharacterInRange.transform.position - transform.position;
        direction.Normalize();
        if (m_IsCharacterInRange != null)
        {
            //direction = transform.position - m_IsCharacterInRange.transform.position; makes the bat escape from you
            direction = m_IsCharacterInRange.transform.position - transform.position;
            direction.Normalize();
            // Debug.Log(direction);
        }
        if (AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0)
        {
            Debug.Log("Attackable enemy was null");
            direction = Vector2.zero;
        }
        SetDirection(direction); //This is the part of the code that actually makes him move
    }

    IEnumerator WaitUntilAbleToAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Attack = true;
    }
}
