using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    //This script will find the appropriate attack from an array and then execute it
    [SerializeField]
    public AttackIdentifier[] attacks;
    AttackIdentifier attackToExecute;

    public System.Random randomNumber = new System.Random();

    public void OnAttack(int attackIndex)
    {
        //For player
        attackToExecute = attacks[attackIndex];
        ExecuteAttack(attackToExecute);
    }
    public float GetStillTime(int attackIndex)
    {
        return attacks[attackIndex].MyCastTime;
    }
    public void OnAttackByEnemy()
    {
        int attackIndex = randomNumber.Next(0, attacks.Length);
        attackToExecute = attacks[attackIndex];
        ExecuteAttack(attackToExecute);
    }
    public void ExecuteAttack(AttackIdentifier attack)
    {
        attackToExecute.GetComponent<AttackParent>().ExecuteAttack(gameObject);
    }
}
