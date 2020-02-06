using UnityEngine;

[CreateAssetMenu(fileName = "New Portrait", menuName = "Portrait")]
public class DialogPortrait : ScriptableObject
{
    //The purpose of this script is to create a portrait for dialogues
    public string fullName;
    public Sprite portrait;
}
