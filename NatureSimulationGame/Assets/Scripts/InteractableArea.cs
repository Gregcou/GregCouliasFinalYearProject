using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArea : MonoBehaviour {

    public bool inRange = false;
    public DialogueManager diaManager;

    public string[] dialogueLines = new string[3];
    public int currentLine = 0;
    public int totalLines;

    public bool hasYesNoChoice = false;

    public PauseMenu pauseScript;

    public int delayYesNoAmount;
    public int delayYesNoCounter = 0;

    void Start ()
    {
        diaManager = FindObjectOfType<DialogueManager>();
        pauseScript = FindObjectOfType<PauseMenu>();
	}
	

	void Update ()
    {

        if (inRange == true && pauseScript.paused == false)
        {
            if (Input.GetKeyUp(KeyCode.Space) && diaManager.isDiaBoxOpen == false)
            {
                diaManager.openDiaBox(dialogueLines[0]);
                if (hasYesNoChoice == true)
                {
                    if (delayYesNoAmount == delayYesNoCounter)
                    {
                        diaManager.openYesNoOption();
                    }
                    delayYesNoCounter++;
                }
                currentLine++;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && diaManager.isDiaBoxOpen == true)
            {
                if (currentLine < totalLines)
                {
                    diaManager.openDiaBox(dialogueLines[currentLine]);
                    currentLine++;
                    if (delayYesNoAmount == delayYesNoCounter && hasYesNoChoice == true) 
                    {
                        diaManager.openYesNoOption();
                    }
                    else if (delayYesNoCounter > delayYesNoAmount)
                    {
                        diaManager.whiteNo.gameObject.SetActive(true);
                        diaManager.whiteYes.gameObject.SetActive(false);
                        diaManager.yesNoText.SetActive(false);
                        diaManager.yesNoOpen = false;
                        diaManager.onYes = false;
                    }
                    delayYesNoCounter++;
                }
                else
                {
                    diaManager.closeDiaBox();
                    if (diaManager.yesNoOpen == true)
                    {
                        diaManager.whiteNo.gameObject.SetActive(true);
                        diaManager.whiteYes.gameObject.SetActive(false);
                        diaManager.yesNoText.SetActive(false);
                        diaManager.yesNoOpen = false;
                        diaManager.onYes = false;
                    }
                    currentLine = 0;
                    delayYesNoCounter = 0;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ore")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Ore")
        {
            inRange = false;
        }
    }

    public void changeDialogue(string newtext, int lineNum)
    {
        dialogueLines[lineNum] = newtext;
    }
}
