using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public static Character Instance;

    public CharacterMovementModel Movement;
    public CharacterInteractionModel Interaction;
    public CharacterMovementView MovementView;
    public CharacterInventoryModel Inventory;
    public CharacterHealthModel Health;
    public CharacterEquipModel Equip;

    public void Awake()
    {
        Instance = this;
    }
}
