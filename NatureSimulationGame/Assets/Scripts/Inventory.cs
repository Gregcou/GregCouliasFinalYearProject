using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int[] inventorySlots = new int[3];
    public string[] itemNames = new string[3];
    public SpriteRenderer slot1;
    public SpriteRenderer slot2;
    public SpriteRenderer slot3;

    public bool slotsFull = false;

    void Start ()
    {
		
	}
	

	void Update ()
    {
        slot1.sprite = Resources.Load<Sprite>("Item" + inventorySlots[0]);
        slot2.sprite = Resources.Load<Sprite>("Item" + inventorySlots[1]);
        slot3.sprite = Resources.Load<Sprite>("Item" + inventorySlots[2]);
    }


    public void setSlot(GameObject itemObject ,bool inWorld)
    {
        bool slotFilled = false;
        int i = 0;
        while (slotFilled == false && slotsFull == false) // if inventory[i] == filled and inventory i == 1 etc set int for that slot + 1 to stack items
        {
            if (inventorySlots[i] == 0)
            {
                if (itemObject.name == "Item1Obj")
                {
                    inventorySlots[i] = 1;
                    itemNames[i] = "Item1Obj";
                    if (inWorld == true)
                    {
                        Destroy(itemObject);
                    }       
                }
                else if (itemObject.name == "Item2Obj")
                {
                    inventorySlots[i] = 2;
                    itemNames[i] = "Item2Obj";
                    if (inWorld == true)
                    {
                        Destroy(itemObject);
                    }
                }
                else if (itemObject.name == "Item3Obj")
                {
                    inventorySlots[i] = 3;
                    itemNames[i] = "Item3Obj";
                    if (inWorld == true)
                    {
                        Destroy(itemObject);
                    }
                }
                slotFilled = true;
            }
            else
            {
                i++;
                if (i == 3)
                {
                    slotsFull = true;
                }
            }
        }
    }


    public bool checkInventory(int itemID)
    {
        if (inventorySlots[0] == itemID)
        {
            inventorySlots[0] = inventorySlots[1];
            inventorySlots[1] = inventorySlots[2];
            inventorySlots[2] = 0;

            itemNames[0] = itemNames[1];
            itemNames[1] = itemNames[2];
            itemNames[2] = "";
            return true;
        }
        else if (inventorySlots[1] == itemID)
        {
            inventorySlots[1] = inventorySlots[2];
            inventorySlots[2] = 0;

            itemNames[1] = itemNames[2];
            itemNames[2] = "";

            return true;
        }
        else if (inventorySlots[2] == itemID)
        {
            inventorySlots[2] = 0;
            itemNames[2] = "";
            return true;
        }
        else
        {
            return false;
        }
    }

    public int checkSlot(int slotNum)
    {
        if (inventorySlots[slotNum] == 1)
        {
            return 1;
        }
        else if (inventorySlots[slotNum] == 2)
        {
            return 2;
        }
        else if (inventorySlots[slotNum] == 3)
        {
            return 3;
        }
        else return 0;
    }

}


// make npc that moves on set path ( looks kinda random but loops)

// combine items maybe 

// make door to go into other room
// store last position in the way to store stuff through scenes
// or look up how to do it without changing scenes
// then set ore stored position

// quit menu button
// save menu button death reloads to previous save

// quest to give item with yes no to hand in

// stacked inventory items like healing items

// put coroutine for attack and player attack so there is time for attack anim

// fix focus not starting on attack in second fight

// ||TASKS THAT REQUIRE GRAPHICS||

// add proper player death action
// weapon switching button with visible switch in corner of screen

 // turn useitem into function then call from each button with slot number as input and in the checkslot field

// invert menu button colours

// take button function out of player into something that exists in every scene