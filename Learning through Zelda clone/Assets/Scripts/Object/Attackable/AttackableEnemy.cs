using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEnemy : AttackableBase{
    public float MaxHealth;
    public GameObject DestroyObjectOnDeath;
    public CharacterMovementModel MovementModel;
    public float HitPushStrenght;
    public float HitPushDuration;
    public GameObject DeathFX;
    public float DelayDeathFX; 
    float m_Health;
    private void Awake()
    {
        m_Health = MaxHealth;
    }

    public float GetHealth()
    {
        return m_Health;
    }
    public float GetMaximumHealth()
    {
        return MaxHealth;
    }
    public float GetHealthPercentage()
    {
      //  Debug.Log("Health is: " + m_Health + " and max health is: " + MaxHealth + " so percentage should be: " + m_Health/MaxHealth);
        return m_Health / MaxHealth;
    }
    public override void OnHit(Collider2D hitcollider, ItemType item, float damage)
    {
        m_Health = m_Health - damage;
        if(MovementModel != null)
        {
            Vector3 pushDirection = transform.position - hitcollider.gameObject.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrenght;
            MovementModel.PushCharacter(pushDirection, HitPushDuration);
        }
        if(m_Health <= 0)
        {
            Debug.Log(DestroyObjectOnDeath.name + " is vanquished");
            StartCoroutine(DestroyRoutine());
            if(DeathFX!=null)
            {
                StartCoroutine(CreateDeathFXRoutine(DelayDeathFX));
            }
        }
        Debug.Log(gameObject.transform.parent.name + " was struck by a " + hitcollider.transform.parent.name);
        Debug.Log("New health: " + m_Health);
    }
    IEnumerator DestroyRoutine()
    {
        GetComponentInParent<CharacterMovementModel>().SetFrozen(true, false, false);
        DialogManager.OnSpeak(GetComponentInParent<CharacterEnemyGeneralControl>().deathDialog);
        yield return new WaitWhile(() => DialogBox.IsVisible());
        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
        Destroy(DestroyObjectOnDeath);
    }

    IEnumerator CreateDeathFXRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(DeathFX, transform.position, Quaternion.identity);
    }

}
