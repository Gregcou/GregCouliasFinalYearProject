using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionArea : MonoBehaviour
{

    public AnimalBehavior animalScript;
    public SquareControl currentSquare;
    public GameObject currentAnimal;
    public int maxHungerValue;
    bool movingToPlant;

    void Start()
    {
        //animalScript = GetComponentInParent<AnimalBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animalScript.movingToObject == true && gameObject.name.Equals("VisionArea"))
        {
            if (currentSquare != null)
            {
               
                if (movingToPlant == true)
                {
                    if (currentSquare.gameObject.tag != "WaterPlant")
                    {
                        Debug.Log("Current square != null");
                        animalScript.movingToObject = false;
                        movingToPlant = false;
                    }
                }
            }
            else if (currentAnimal == null || currentAnimal.GetComponent<AnimalBehavior>().canHaveChild == false)
            {
                Debug.Log("Turn off moving towards animal is null");
                animalScript.movingToObject = false;
                currentAnimal.GetComponent<AnimalBehavior>().beingMovedTowards = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.name.Equals("VisionArea"))
        {
            if (animalScript.movingToObject == false)
            {
                if (other.gameObject.tag == "WaterPlant")
                {
                    if (animalScript.hungerLevel < maxHungerValue)
                    {
                        Debug.Log("vision collision");
                        currentSquare = other.gameObject.GetComponent<SquareControl>();
                        animalScript.moveToObject(other.transform.position);
                        movingToPlant = true;
                    }
                }
                else if (other.gameObject.tag == "Turtle")
                {
                    if (other.GetComponent<AnimalBehavior>().canHaveChild == true && animalScript.canHaveChild == true && animalScript.beingMovedTowards == false)
                    {
                        other.GetComponent<AnimalBehavior>().beingMovedTowards = true;
                        Debug.Log("other turtle collision");
                        currentAnimal = other.gameObject;
                        animalScript.moveToObject(other.transform.position);
                        other.GetComponent<AnimalBehavior>().moveSpeed = 0;
                    }
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name.Equals("ReachArea"))
        {
            if (other.gameObject.tag == "WaterPlant")
            {
                if (animalScript.hungerLevel < maxHungerValue)
                {
                    Debug.Log("reach area collision");
                    other.gameObject.GetComponent<SquareControl>().eatPlant("plantDeath");
                    animalScript.hungerLevel += 1;
                    animalScript.movingToObject = false;
                    movingToPlant = false;
                }
            }
            else if (other.gameObject.tag == "Turtle")
            {
                if (other.GetComponent<AnimalBehavior>().canHaveChild == true && animalScript.canHaveChild == true)
                {
                    Debug.Log("other turtle reach area collision");
                    other.GetComponent<AnimalBehavior>().beingMovedTowards = false;
                    animalScript.haveChild();
                    other.GetComponent<AnimalBehavior>().canHaveChild = false;
                    animalScript.movingToObject = false;
                    other.GetComponent<AnimalBehavior>().moveSpeed = 0.5f;
                }
            }
        }
        
    }



}
