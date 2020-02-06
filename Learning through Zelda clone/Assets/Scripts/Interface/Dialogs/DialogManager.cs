using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    static DialogManager Instance;
    static Dialog myDialog = null;
    private static int dialogLine = 0;

    private void Awake()
    {
        Instance = this;
    }
    //This script will get a dialog from an NPC and then put it into the dialog box
    public static void OnSpeak(Dialog currentDialog)
    {
        myDialog = currentDialog;
        DoSpeak();
    }
    public static void DoSpeak()
    {
        if(DialogBox.isPrinting == false)
        {
            DialogBox.EmptyDialogBox();
            if (dialogLine < myDialog.GetAmountOfLines())
            {
                DialogBox.Show(myDialog.lines[dialogLine].text);
                dialogLine += 1;
            }
            else
            {
                myDialog = null;
                dialogLine = 0;
                DialogBox.Hide();
            }
        }
        else
        {
            DialogBox.printAll = true;
        }
    }
}
