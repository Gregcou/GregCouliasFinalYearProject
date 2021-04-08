using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionArea : MonoBehaviour
{

    Turtle turtleScript;
    SquareControl currentSquare;
    // Start is called before the first frame update
    void Start()
    {
        turtleScript = GetComponentInParent<Turtle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turtleScript.movingToObject == true)
        {
            if (currentSquare != null)
            {
                if (currentSquare.gameObject.tag != "WaterPlant")
                {
                    turtleScript.movingToObject = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (turtleScript.hungerLevel < 3)
        {
            if (gameObject.name.Equals("VisionArea"))
            {
                if (turtleScript.movingToObject == false)
                {
                    if (other.gameObject.tag == "WaterPlant")
                    {
                        Debug.Log("vision collision");
                        currentSquare = other.gameObject.GetComponent<SquareControl>();
                        turtleScript.moveToObject(other.transform.position);
                    }
                }
            }
            else if (gameObject.name.Equals("ReachArea"))
            {
                if (other.gameObject.tag == "WaterPlant")
                {
                    Debug.Log("reach area collision");
                    currentSquare.eatPlant("plantDeath");
                    turtleScript.hungerLevel += 1;
                    turtleScript.movingToObject = false;
                }
            }
        }
    }

    
}
