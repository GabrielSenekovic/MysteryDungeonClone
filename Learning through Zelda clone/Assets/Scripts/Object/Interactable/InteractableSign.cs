using UnityEngine;
using System.Collections;

public class InteractableSign : InteractableBase
{
    void Awake()
    {
        IsInteractable = true;
    }
    public string Text;
    public override void OnInteract(Character character)
    {
        Debug.Log("Interact with Sign");
        if(DialogBox.IsVisible() == true)
        {
            Character.Instance.Movement.SetFrozen(false, false, true);
            DialogBox.Hide();
        }
        else
        {
            Character.Instance.Movement.SetFrozen(true, true, true);
            DialogBox.Show(Text);
        }
    }
  /*  IEnumerator FreezeTimeRoutine()
    {
        yield return null;
        Time.timeScale = 0;
    }*/
}