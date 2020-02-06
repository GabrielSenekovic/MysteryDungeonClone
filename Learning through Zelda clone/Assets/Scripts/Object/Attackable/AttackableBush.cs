using UnityEngine;
using System.Collections;

public class AttackableBush : AttackableBase {

    public GameObject DestroyedPrefab;
    public GameObject DestroyEffect;
    private SpriteRenderer m_Renderer;

    void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void CreateDestroyedPrefab()
    {
        //Instantiate(DestroyedPrefab,transform.position,transform.rotation); This instantiates the object, but not in the parent
        GameObject newDestroyedBushObject = Instantiate(DestroyedPrefab, transform.position, transform.rotation);
        newDestroyedBushObject.transform.parent = transform.parent;
    }
    public void DropLoot() //Its best if a method only does one thing
    {
        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
    }
    public void DestroyBush()
    {
        Destroy(gameObject);
        if (DestroyEffect != null)
        {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
            destroyEffect.transform.position = transform.position;
        }
    }
    public override void OnHit(Collider2D collider, ItemType item, float damage)
        //This should get caught when the object is hit
    {
        DestroyBush();
        CreateDestroyedPrefab();
        DropLoot();
    }
    void OnPickupObject(Character character)
    {
        CreateDestroyedPrefab();
        DropLoot();
    }
    void OnObjectThrown()
    {
        DestroyBush();
    }
}
