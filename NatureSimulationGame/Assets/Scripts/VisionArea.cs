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

    }

    void Update()
    {
        // checks if animal or plant has died while moving towards it and stops if it does
        if (animalScript.movingToObject == true && gameObject.name.Equals("VisionArea"))
        {
            if (currentSquare != null)
            {
               
                if (movingToPlant == true)
                {
                    if (currentSquare.gameObject.tag == "Square")
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
                // finds food or another animal and starts the animal moving towards it
                if (other.gameObject.tag == "WaterPlant" && animalScript.animalName == "Turtle")
                {
                    if (animalScript.hungerLevel < maxHungerValue)
                    {
                        Debug.Log("vision collision");
                        currentSquare = other.gameObject.GetComponent<SquareControl>();
                        animalScript.moveToObject(other.transform.position);
                        movingToPlant = true;
                    }
                }
                else if (other.gameObject.tag == "Grass" && animalScript.animalName == "Pig")
                {
                    if (animalScript.hungerLevel < maxHungerValue)
                    {
                        Debug.Log("vision collision");
                        currentSquare = other.gameObject.GetComponent<SquareControl>();
                        animalScript.moveToObject(other.transform.position);
                        movingToPlant = true;
                    }
                }
                else if (other.gameObject.tag == "Turtle" && animalScript.animalName == "Turtle")
                {
                    if (other.GetComponent<AnimalBehavior>().canHaveChild == true && animalScript.canHaveChild == true && animalScript.beingMovedTowards == false)
                    {
                        other.GetComponent<AnimalBehavior>().beingMovedTowards = true;
                        Debug.Log("other turtle collision");
                        currentAnimal = other.gameObject;
                        animalScript.moveToObject(other.transform.position);
                        // stop the second animal so they can reach eachother and have a child
                        other.GetComponent<AnimalBehavior>().moveSpeed = 0;
                    }
                }
                else if (other.gameObject.tag == "Pig" && animalScript.animalName == "Pig")
                {
                    if (other.GetComponent<AnimalBehavior>().canHaveChild == true && animalScript.canHaveChild == true && animalScript.beingMovedTowards == false)
                    {
                        other.GetComponent<AnimalBehavior>().beingMovedTowards = true;
                        Debug.Log("other pig collision");
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
        // when animal reaches its target either eat the plant or have a child
        if (gameObject.name.Equals("ReachArea"))
        {
            if (other.gameObject.tag == "WaterPlant" && animalScript.animalName == "Turtle")
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
            else if (other.gameObject.tag == "Grass" && animalScript.animalName == "Pig")
            {
                if (animalScript.hungerLevel < maxHungerValue)
                {
                    Debug.Log("reach area collision");
                    other.gameObject.GetComponent<SquareControl>().eatOrPickUpGrass();
                    animalScript.hungerLevel += 1;
                    animalScript.movingToObject = false;
                    movingToPlant = false;
                }
            }
            else if (other.gameObject.tag == "Turtle" && animalScript.animalName == "Turtle")
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
            else if (other.gameObject.tag == "Pig" && animalScript.animalName == "Pig")
            {
                if (other.GetComponent<AnimalBehavior>().canHaveChild == true && animalScript.canHaveChild == true)
                {
                    Debug.Log("other turtle reach area collision");
                    other.GetComponent<AnimalBehavior>().beingMovedTowards = false;
                    animalScript.haveChild();
                    other.GetComponent<AnimalBehavior>().canHaveChild = false;
                    animalScript.movingToObject = false;
                    other.GetComponent<AnimalBehavior>().moveSpeed = 1.5f;
                }
            }
        }
        
    }



}
