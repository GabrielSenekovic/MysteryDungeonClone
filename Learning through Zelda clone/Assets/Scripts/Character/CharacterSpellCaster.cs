using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpellCaster : ProjectileSpawner {
    private CharacterMovementModel m_MovementModel;

    public void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    public override void OnSpawn()
    {
        foreach (ProjectilePrefab projectile in ProjectilePrefabs)
        {
            /*
            Okay so to get the component from an object in the list, I need to take it from the name I assign it in the foreach loop
             */
            Vector3 startingPosition = transform.position + projectile.SpawnPosition; //The position where the projectile spawns
            Vector3 facingDirection = m_MovementModel.GetFacingDirection();
            projectile.TargetDirection = facingDirection;
            if ( facingDirection.x == 0 && facingDirection.y == -1)
            {
                startingPosition = new Vector3 (startingPosition.x,startingPosition.y -1);
            }
            else if (facingDirection.x == 0 && facingDirection.y == 1)
            {
                startingPosition = new Vector3(startingPosition.x, startingPosition.y + 1);
            }
            else if (facingDirection.x == 1 && facingDirection.y ==0)
            {
                startingPosition = new Vector3(startingPosition.x + 1, startingPosition.y);
            }
            else if (facingDirection.x == -1 && facingDirection.y ==0)
            {
                startingPosition = new Vector3(startingPosition.x - 1, startingPosition.y);
            }

            projectile.Prefab.GetComponent<ProjectileMovement>().startingPosition = startingPosition;
            projectile.Prefab.GetComponent<ProjectileMovement>().targetDirection = projectile.TargetDirection;
            projectile.Prefab.GetComponent<ProjectileMovement>().Speed = projectile.Speed;

            GameObject prefabInstance = Instantiate(projectile.Prefab, startingPosition, Quaternion.identity); //First we make the projectile appear
            prefabInstance.GetComponent<ProjectileMovement>().OnSpawn();
        }
    }
    //However this spellcaster will get all of its functionality from a spellbook enumerator
}
