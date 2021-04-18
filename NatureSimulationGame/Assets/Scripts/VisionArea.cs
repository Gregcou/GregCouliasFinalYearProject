using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionArea : MonoBehaviour
{

    public AnimalBehavior animalScript;
    public SquareControl currentSquare;
    public int maxHungerValue;

    void Start()
    {
        //animalScript = GetComponentInParent<AnimalBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animalScript.movingToObject == true)
        {
            if (currentSquare != null)
            {
                if (currentSquare.gameObject.tag != "WaterPlant")
                {
                    animalScript.movingToObject = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (animalScript.hungerLevel < maxHungerValue)
        {
            if (gameObject.name.Equals("VisionArea"))
            {
                if (animalScript.movingToObject == false)
                {
                    if (other.gameObject.tag == "WaterPlant")
                    {
                        Debug.Log("vision collision");
                        currentSquare = other.gameObject.GetComponent<SquareControl>();
                        animalScript.moveToObject(other.transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animalScript.hungerLevel < maxHungerValue)
        {
            if (gameObject.name.Equals("ReachArea"))
            {
                if (other.gameObject.tag == "WaterPlant")
                {
                    Debug.Log("reach area collision");
                    other.gameObject.GetComponent<SquareControl>().eatPlant("plantDeath");
                    animalScript.hungerLevel += 1;
                    animalScript.movingToObject = false;
                }
            }
        }
    }



}
