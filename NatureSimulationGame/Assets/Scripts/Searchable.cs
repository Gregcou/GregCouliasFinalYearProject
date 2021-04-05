using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour {

    public GameObject itemPrefab;
    public InteractableArea dialogueScript;
    public Inventory inventoryScript;
    bool itemTaken = false;

    public string noLine3;
    public string yesLine3;
    public string takenLine3;

	// Use this for initialization
	void Start ()
    {
        dialogueScript = GetComponentInParent<InteractableArea>();
        inventoryScript = GameObject.Find("InventoryManager").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueScript.inRange == true)
            {
                if (itemTaken == false)
                {
                    if (dialogueScript.diaManager.onYes == true)
                    {
                        inventoryScript.setSlot(itemPrefab, false);
                        itemTaken = true;
                        dialogueScript.changeDialogue(yesLine3, 2);
                    }
                    else if (dialogueScript.diaManager.onYes == false && dialogueScript.currentLine == 2)
                    {
                        dialogueScript.changeDialogue(noLine3, 2);
                    }
                }
                else if (itemTaken == true)
                {
                    dialogueScript.changeDialogue(takenLine3, 2);
                }
            }
        }
	}
}
