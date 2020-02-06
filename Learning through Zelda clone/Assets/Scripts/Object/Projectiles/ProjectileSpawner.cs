using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public List<ProjectilePrefab> ProjectilePrefabs;
    int AmountOfProjectiles = 0;
    public bool IsSpawning;
    public bool IsRotating;
    public bool IsRotatingCounterClockwise;
    public bool RotateOnePerTime;
    public bool PeriodicSpawning;
    public float TimeBetweenSpawn;

    public void Start()
    {
        if (PeriodicSpawning == true)
        {
            StartCoroutine(OnPeriodicSpawn());
        }
        if (IsRotating == true)
        {
            StartCoroutine(OnRotation());
        }
    }

    public void Update()
    {
        if(IsSpawning == true)
        {
            OnSpawn();
        }
    }

    virtual public void OnSpawn()
    {
        foreach(ProjectilePrefab projectile in ProjectilePrefabs)
        {
            /*
            Okay so to get the component from an object in the list, I need to take it from the name I assign it in the foreach loop
             */
            Vector3 startingPosition = transform.position + projectile.SpawnPosition; //The position where the projectile spawns

            projectile.Prefab.GetComponent<ProjectileMovement>().startingPosition = startingPosition;
            projectile.Prefab.GetComponent<ProjectileMovement>().targetDirection = projectile.TargetDirection;
            projectile.Prefab.GetComponent<ProjectileMovement>().Speed = projectile.Speed;

            GameObject prefabInstance = Instantiate(projectile.Prefab, startingPosition, Quaternion.identity); //First we make the projectile appear
            prefabInstance.GetComponent<ProjectileMovement>().OnSpawn();
        }
        IsSpawning = false;
    }

    virtual public void OnSpawnByCharacter(GameObject whoIsCasting)
    {
        foreach (ProjectilePrefab projectile in ProjectilePrefabs)
        {
            Vector3 startingPosition;
            if (whoIsCasting.transform.parent.transform.parent.gameObject.tag == "Enemy")
            {
                Vector3 myPosition = new Vector3(whoIsCasting.transform.position.x + whoIsCasting.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset.x, whoIsCasting.transform.position.y + whoIsCasting.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>().offset.y, 1);
                startingPosition = myPosition + projectile.SpawnPosition;
                if(projectile.Prefab.GetComponentInChildren<CircleCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(projectile.Prefab.GetComponentInChildren<CircleCollider2D>(), whoIsCasting.transform.parent.GetComponentInParent<CharacterEnemyGeneralControl>().MyBodyCollision.GetComponent<BoxCollider2D>());
                }
              //  Debug.Log("Caster is an enemy, so Im putting the weapon collider to false");
            }
            else if(whoIsCasting.transform.parent.gameObject.tag == "Player")
            {
                Vector3 myPosition = new Vector3(whoIsCasting.transform.position.x + whoIsCasting.GetComponentInParent<BoxCollider2D>().offset.x, whoIsCasting.transform.position.y + whoIsCasting.GetComponentInParent<BoxCollider2D>().offset.y, 1);
                startingPosition = myPosition + projectile.SpawnPosition;
                Physics2D.IgnoreCollision(projectile.Prefab.GetComponentInChildren<CircleCollider2D>(), whoIsCasting.GetComponentInParent<BoxCollider2D>());
            }
            else
            {
             //   Debug.Log("Game Object had no tag at all!");
                return;
            }
            //here I add the spawn position and adds it to the position of the character. Basically, this puts it on the character, because most often there is no spawn position on the particle itself
            Vector3 direction = whoIsCasting.transform.parent.GetComponentInParent<CharacterMovementModel>().GetFacingDirection();
            projectile.TargetDirection = direction;
            if (direction.x >= -0.5 & direction.y <= -0.5)
            {
                //south
                startingPosition = new Vector3(startingPosition.x, startingPosition.y - 1);
            }
            else if (direction.x <= 0.5 & direction.y >= 0.5)
            {
                //north
                startingPosition = new Vector3(startingPosition.x, startingPosition.y + 1);
            }
            else if (direction.x >= 0.5 & direction.y >= 0.5)
            {
                //northeast
                startingPosition = new Vector3(startingPosition.x +1, startingPosition.y + 1);
            }
            else if (direction.x <= -0.5 & direction.y >= 0.5)
            {
                //northwest
                startingPosition = new Vector3(startingPosition.x -1, startingPosition.y + 1);
            }
            else if (direction.x >= 0.5 & direction.y <= -0.5)
            {
                //southeast
                startingPosition = new Vector3(startingPosition.x + 1, startingPosition.y - 1);
            }
            else if (direction.x <= -0.5 & direction.y <= -0.5)
            {
                //southwest
                startingPosition = new Vector3(startingPosition.x -1, startingPosition.y - 1);
            }
            else if (direction.x >= 0.5 & direction.y <= 0.5)
            {
                //east
                startingPosition = new Vector3(startingPosition.x + 1, startingPosition.y);
            }
            else if (direction.x <= -0.5 & direction.y >= -0.5)
            {
                //west
                startingPosition = new Vector3(startingPosition.x - 1, startingPosition.y);
            }
            else
            {
                Debug.Log("Code fucked up somehow");
            }

            projectile.Prefab.GetComponent<ProjectileMovement>().startingPosition = startingPosition;
            projectile.Prefab.GetComponent<ProjectileMovement>().targetDirection = projectile.TargetDirection;
            projectile.Prefab.GetComponent<ProjectileMovement>().Speed = projectile.Speed;
            projectile.Prefab.GetComponent<ProjectileMovement>().caster = whoIsCasting;

            GameObject prefabInstance = Instantiate(projectile.Prefab, startingPosition, Quaternion.identity); //First we make the projectile appear
            prefabInstance.GetComponent<ProjectileMovement>().OnSpawn();
        }
    }

   
    public void OnDrawGizmos()
    {
        foreach(ProjectilePrefab projectile in ProjectilePrefabs)
        {
            Vector3 startingPosition = transform.position + projectile.SpawnPosition;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(startingPosition, new Vector3(1, 1, 0));
            Gizmos.DrawLine(startingPosition, startingPosition + projectile.TargetDirection);
        }
    }

    IEnumerator OnPeriodicSpawn()
    {
        while (PeriodicSpawning == true)
        {
            OnSpawn();
            yield return new WaitForSeconds(TimeBetweenSpawn);
        }
    }

    IEnumerator OnRotation()
    {
        if (IsRotatingCounterClockwise == true)
        {
            while (IsRotatingCounterClockwise == true)
            {
                foreach (ProjectilePrefab projectile in ProjectilePrefabs)
                {
                    Vector3 previousPosition = new Vector3(projectile.SpawnPosition.x, projectile.SpawnPosition.y);
                    projectile.SpawnPosition.x = -previousPosition.y;
                    projectile.SpawnPosition.y = previousPosition.x;
                    Vector3 previousTarget = new Vector3(projectile.TargetDirection.x, projectile.TargetDirection.y);
                    projectile.TargetDirection.x = -previousTarget.y;
                    projectile.TargetDirection.y = previousTarget.x;
                    if(RotateOnePerTime == true)
                    {
                        yield return new WaitForSeconds(TimeBetweenSpawn);
                    }
                }
                if(RotateOnePerTime == false)
                {
                yield return new WaitForSeconds(TimeBetweenSpawn);
                }
            
            }
        }
        else
        {
            while (IsRotating == true)
            {
                foreach (ProjectilePrefab projectile in ProjectilePrefabs)
                {
                    Vector3 previousPosition = new Vector3(projectile.SpawnPosition.x, projectile.SpawnPosition.y);
                    projectile.SpawnPosition.x = previousPosition.y;
                    projectile.SpawnPosition.y = -previousPosition.x;
                    Vector3 previousTarget = new Vector3(projectile.TargetDirection.x, projectile.TargetDirection.y);
                    projectile.TargetDirection.x = previousTarget.y;
                    projectile.TargetDirection.y = -previousTarget.x;
                    if (RotateOnePerTime == true)
                    {
                        yield return new WaitForSeconds(TimeBetweenSpawn);
                    }
                }
                if (RotateOnePerTime == false)
                {
                    yield return new WaitForSeconds(TimeBetweenSpawn);
                }
            }
        }
        
        
    }
}


[System.Serializable]
public class ProjectilePrefab
{
    public GameObject Prefab;
    public Vector3 SpawnPosition;
    public Vector3 TargetDirection;
    public float Speed;
    /*
     * 1,0,0 = Right
     * -1,0,0 = Left
     * 0,1,0 = Up
     * 0,-1,0 = Down
     */
}