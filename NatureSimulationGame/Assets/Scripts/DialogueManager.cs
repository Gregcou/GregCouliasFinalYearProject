using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject diaBox;
    public Text dialogueText;
    public bool isDiaBoxOpen = false;


    public Player playerMovementScript;
    public Animator playerAnimator;

    public bool yesNoOpen = false;
    public bool onYes = false;
    public GameObject yesNoText;
    public Text whiteYes;
    public Text whiteNo;
    public bool yesNoChosen = false;

    public PauseMenu pauseScript;

    

    // Use this for initialization
    void Start()
    {
        playerMovementScript = GameObject.Find("Ore").GetComponent<Player>();
        playerAnimator = GameObject.Find("Ore").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (yesNoOpen == true)
        {
            if (onYes == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (pauseScript.paused == false)
                    {
                        onYes = true;
                        whiteNo.gameObject.SetActive(false);
                        whiteYes.gameObject.SetActive(true);
                    } 
                }
            }

            if (onYes == true)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    if (pauseScript.paused == false)
                    {
                        onYes = false;
                        whiteNo.gameObject.SetActive(true);
                        whiteYes.gameObject.SetActive(false);
                    }
                }
            }
        }
        
    }

    public void openDiaBox(string dialogue)
    {
        dialogueText.text = dialogue;
        diaBox.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        isDiaBoxOpen = true;
        playerMovementScript.enabled = false;
        playerAnimator.SetBool("Walking", false);
        playerAnimator.SetBool("WalkingBack", false);
        playerAnimator.SetBool("WalkingSideRight", false);
        playerAnimator.SetBool("WalkingSideLeft", false);

    }

    public void closeDiaBox()
    {
        diaBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        isDiaBoxOpen = false;
        playerMovementScript.enabled = true;
    }

    public void openYesNoOption() 
    {
        yesNoOpen = true;
        yesNoText.SetActive(true);
        playerMovementScript.enabled = false;
    }
}
