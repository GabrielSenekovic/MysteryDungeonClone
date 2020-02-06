using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemyGeneralControl : CharacterMovementBaseControl {
    public float PushStrength;
    public float PushTime;
    public float AttackDamage;
    public GameObject m_IsCharacterInRange;
    public AttackableEnemy AttackableEnemy;
    public EnemyCharacterCollision MyBodyCollision;
    public bool IsRotating;
    public float RotationSpeed;
    public RoomParent myRoom; //let this be set by the same script that spawns the boss

    public Dialog deathDialog;
    public bool InitiatedDialog = false;

    virtual public void Update()
    {
        if(IsRotating == true)
        {
            RotateCharacter();
        }
    }
    public void SetCharacterInRange(GameObject characterInRange)
    {
        m_IsCharacterInRange = characterInRange;
    }
    virtual public void OnHitCharacter(Character character)
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();
      //  m_IsCharacterInRange = null;
        Character.Instance.Movement.PushCharacter(direction * PushStrength, PushTime);
        Character.Instance.Health.DealDamage(AttackDamage);
    }
    public void RotateCharacter()
    {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
