using UnityEngine;

[System.Serializable]
public struct Line
{
    //Wow ive never done this before
    public DialogPortrait portrait;
    [TextArea(2, 5)] //This means the text is a specific size, in the inspector that is
    public string text;
}

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public DialogPortrait speakerLeft;
    public DialogPortrait speakerRight;
    public Line[] lines;

    public int GetAmountOfLines()
    {
        return lines.Length;
    }
}
