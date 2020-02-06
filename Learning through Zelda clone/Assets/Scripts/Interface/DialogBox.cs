using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogBox : MonoBehaviour {

    static DialogBox Instance;
    private Image m_DialogFrame;
    private Text m_Text;
    [SerializeField]
    protected GameObject m_TextArea;

    private static char previouslyPrintedLetter = '\0';
    private static float previouslyPrintedLetterLocation = 0;
    private static float totalLineLength = 0;
    private static float letterCount = 0;
    private static float lineY = 0;

    public static bool isPrinting = false;
    public static bool printAll = false;

    static List<GameObject> currentlyDisplayedLetters = new List<GameObject>();

    void Awake ()
    {
        Instance = this;
        m_DialogFrame = GetComponent<Image>();
        m_Text = GetComponentInChildren<Text>();
    }

    public static void Show(string displayText)
    {
        Character.Instance.Movement.SetFrozen(true, true, true);
        Instance.DoShow(displayText);
    }

    public static void Hide()
    {
        Debug.Log(Character.Instance.Movement.IsFrozen());
        Character.Instance.Movement.SetFrozen(false, false, true);
        Debug.Log(Character.Instance.Movement.IsFrozen());
        Instance.DoHide();
    }

    public static bool IsVisible()
    {
        return Instance.m_DialogFrame.enabled;
    }

    void DoHide()
    {
        EmptyDialogBox();
        m_DialogFrame.enabled = false;
        UIManager.MyInstance.RevealUI();
        // m_Text.enabled = false;
    }

    void DoShow(string displayText)
    {
        isPrinting = true;
        printAll = false;
        UIManager.MyInstance.HideUI();
        lineY = -6;
        m_DialogFrame.enabled = true;
        StartCoroutine(PrintText(displayText));
     //   m_Text.enabled = true;
      //  m_Text.text = displayText;
    }
    void ChangeText(string displayText)
    {
        //  m_Text.text = displayText;
        StartCoroutine(PrintText(displayText));
    }

    IEnumerator PrintText(string displayText)
    {
        foreach (char c in displayText)
        {
            CheckWord(displayText, c);

            PrintChar(c);
            if (printAll != true)
            {
                if (c != ' ')
                {
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }
        isPrinting = false;
      //  Debug.Log("The totalLineLength is: " + totalLineLength);
    }

    void PrintChar(char displayLetter)
    {
      //  Debug.Log("Printing: " + displayLetter);

        Vector3 nextLocation = new Vector3(0, lineY, 0);
        nextLocation.x = FindNextLocationForLetter();
        letterCount += 1;

        if (displayLetter != ' ')
        {
            if(letterCount > 1)
            {
                if(previouslyPrintedLetter != ' ')
                {
                 //   Debug.Log("Adding to total line");
                    totalLineLength += Database.Letter.FindLetter(previouslyPrintedLetter).nextPositionOffset; //this should ideally never affect the letter first on a line
                  //  Debug.Log(totalLineLength);
                }
                //it says not space, because then it starts looking for a space in the database of which there is none
                else
                {
                 //   Debug.Log("this is after a space, adding to total line");
                    totalLineLength += 7;
                }
            }
            if(totalLineLength >= 248)
            {
             //   Debug.Log("Time to switch line!");
                switchToNewLine();
            }
            LetterData letterData = Database.Letter.FindLetter(displayLetter);
            GameObject letterPrefab = Instantiate(letterData.Prefab, nextLocation, Quaternion.identity);
            letterPrefab.transform.SetParent(m_TextArea.transform, false);
            currentlyDisplayedLetters.Add(letterPrefab);
        }
        else
        {
            if(totalLineLength != 248)
            {
              //  Debug.Log("Now taking care of a space");
                totalLineLength += 5;
                if (totalLineLength >= 250)
                {
                 //   Debug.Log("Time to switch line!");
                    switchToNewLine();
                }
                nextLocation.x += 5;
            }
        }
        previouslyPrintedLetter = displayLetter;
        previouslyPrintedLetterLocation = nextLocation.x; //Adding this ensures that the letters go forward each time
    }

    void CheckWord(string displayText, char firstLetterOFWord)
    {
        float whatLineLengthWillBecome = totalLineLength;
        char previouslyCheckedChar = '\0';

        if (previouslyPrintedLetter == ' '  || previouslyPrintedLetter == '\0')
        {
            string word = "";
            foreach (char letterOfWord in displayText.Substring((int)letterCount))
            {
             //   Debug.Log(letterOfWord);
                if (letterOfWord == ' ')
                {
                    break;
                }
                if(previouslyCheckedChar != '\0')
                {
                    whatLineLengthWillBecome += Database.Letter.FindLetter(previouslyCheckedChar).nextPositionOffset;
                }
                previouslyCheckedChar = letterOfWord;
                word += letterOfWord;
            }
          //  Debug.Log("The word is: " + word);
          //  Debug.Log("The line will become " + whatLineLengthWillBecome);
            if(whatLineLengthWillBecome >= 248)
            {
           //     Debug.Log("Time to switch line!");
                switchToNewLine();
            }
        }
    }


    float FindNextLocationForLetter()
    {
        if(previouslyPrintedLetter != '\0' && previouslyPrintedLetter != ' ' && letterCount != 0)
        {
         //   Debug.Log("The previously printed letter is: " + previouslyPrintedLetter);
       //     Debug.Log("LetterCount is: " + letterCount);
         //   Debug.Log("This is not the first letter");
            float offset = Database.Letter.FindLetter(previouslyPrintedLetter).nextPositionOffset + previouslyPrintedLetterLocation;
         //   Debug.Log("The previous letters x was: " + previouslyPrintedLetterLocation);
          //  Debug.Log("The offset is: " + offset);
            return offset;
        }

        else if(previouslyPrintedLetter != '\0' /* because it may never happen at the start of a sentence */
            && previouslyPrintedLetter == ' ' /* because it may only happen after a space */
            && totalLineLength != 0) 
        {
            return previouslyPrintedLetterLocation;
        }

        else
        {
     //       Debug.Log("This is the first letter in this line");
            return -125;
        }
    }

    void switchToNewLine()
    {
        totalLineLength = 0;
        previouslyPrintedLetterLocation = 0;
        letterCount = 0;
        lineY -= 12;
        m_TextArea.GetComponent<RectTransform>().sizeDelta = new Vector2(m_TextArea.GetComponent<RectTransform>().sizeDelta.x, m_TextArea.GetComponent<RectTransform>().sizeDelta.y + 12);
        previouslyPrintedLetter = '\0';
        GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0;
        // Debug.Log("Line has been switched");
    }

    public static void EmptyDialogBox()
    {
        foreach (GameObject c in currentlyDisplayedLetters)
        {
            Object.Destroy(c);
        }
        totalLineLength = 0;
        previouslyPrintedLetterLocation = 0;
        letterCount = 0;
        lineY = -6;
        previouslyPrintedLetter = '\0';
        Instance.m_TextArea.GetComponent<RectTransform>().sizeDelta = new Vector2(Instance.m_TextArea.GetComponent<RectTransform>().sizeDelta.x, 12);
        Instance.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
    }
}
