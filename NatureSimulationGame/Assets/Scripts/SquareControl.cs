using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareControl : MonoBehaviour
{
    public int squareNum;
    public SpriteRenderer sr;
    public int currentState = 0;
    public GridManager manager;
    int gridSize;
    public List<int> neighbours = new List<int>();
    public List<int> neighboursStates = new List<int>();
    public List<int> oldNeighboursStates = new List<int>();
    public int neighboursStateTotal = 0;
    public float updatePeriod;
    int waterValue = 3;
    int waterPlantValue = 4;
    int dyingWaterPlantValue = 5;
    int sunDialValue = 7;
    int standaloneBushValue = 8;
    int edgeBushValue = 9;
    int cattleGridValue = 10;
    int doorValue = 11;
    public Sprite[] sprites;
    public Animator animator;
    int timeToAdd = 0;
    public int WaterPlantLifeLength;
    public CircleCollider2D bushCollider;
    public PolygonCollider2D sundDialCollider;
    public CapsuleCollider2D edgeBushCollider;
    public bool inSecondArea = false;


    void Start()
    {
        manager = GameObject.Find("SquareManager").GetComponent<GridManager>();
        gridSize = manager.gridSize;
        
        // use the set of states for the correct area the square is in
        if (inSecondArea == false)
        {
            currentState = manager.squaresStates[squareNum];
        }
        else if (inSecondArea == true)
        {
            currentState = manager.squaresStatesArea2[squareNum];
        }

        // when spawned the square checks the states from the manager and becomes the right type
        if (currentState == waterValue)
        {
            turnWater();
        }
        else if (currentState == waterPlantValue)
        {
            turnWaterPlant();
        }
        else if (currentState == sunDialValue)
        {
            turnSunDial();
        }
        else if (currentState == standaloneBushValue)
        {
            turnStandaloneBush();
        }
        else if (currentState == edgeBushValue)
        {
            turnEdgeBush();
        }
        else if (currentState == cattleGridValue)
        {
            turnCattleGrid();
        }
        else if (currentState == doorValue)
        {
            turnDoor();
        }

        findNeighbours();

        // get the neighbours states so they can be checked
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (inSecondArea == false)
            {
                neighboursStates.Add(manager.squaresStates[neighbours[i]]);
            }
            else if (inSecondArea == true)
            {
                neighboursStates.Add(manager.squaresStatesArea2[neighbours[i]]);
            }

            if (neighboursStates[i] == 1)
            {
                neighboursStateTotal += 1;
            }
        }

        if (currentState == 1)
        {
            turnOn();
        }

        StartCoroutine(checkState());
        

    }

    void Update()
    {
        // constantly update the neighbour states from the manager
        for (int i = 0; i < neighbours.Count; i++)
        {
            
            if (inSecondArea == false)
            {
                neighboursStates[i] = manager.squaresStates[neighbours[i]];
            }
            else if (inSecondArea == true)
            {
                neighboursStates[i] = manager.squaresStatesArea2[neighbours[i]];
            }
        }

        // change the sundial sprite based on time of day to move it around
        if (currentState == sunDialValue)
        {
            if (manager.worldTimeSeconds >= ((manager.daylength/8)) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 2) + timeToAdd)
            {
                sr.sprite = sprites[15];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 2) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 3) + timeToAdd)
            {
                sr.sprite = sprites[16];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 3) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 4) + timeToAdd)
            {
                sr.sprite = sprites[17];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 4) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 5) + timeToAdd)
            {
                sr.sprite = sprites[18];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 5) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 6) + timeToAdd)
            {
                sr.sprite = sprites[19];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 6) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 7) + timeToAdd)
            {
                sr.sprite = sprites[20];
            }
            else if (manager.worldTimeSeconds >= ((manager.daylength / 8) * 7) + timeToAdd && manager.worldTimeSeconds <= ((manager.daylength / 8) * 8) + timeToAdd)
            {
                sr.sprite = sprites[21];
                
            }
            else if (manager.worldTimeSeconds >= manager.daylength + timeToAdd)
            {
                sr.sprite = sprites[14];
                timeToAdd += manager.daylength;
                Debug.Log(timeToAdd);
                Debug.Log(manager.worldTimeSeconds);
            }
            


        }
    }

    // turns the square to the correct state and updates the animator
    void turnOn()
    {
        currentState = 1;
        animator.SetInteger("SquareState", 1);
        gameObject.tag = "Grass";
        
        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = 1;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = 1;
        }
    }

    void turnOff()
    {
        currentState = 0;
        animator.SetInteger("SquareState", 0);
        gameObject.tag = "Square";

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = 0;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = 0;
        }
        if (bushCollider.enabled == true)
        {
            bushCollider.enabled = false;
        }
    }

    void turnWater()
    {
        currentState = waterValue;
        animator.SetInteger("SquareState", 3);
        bushCollider.enabled = true;

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = waterValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = waterValue;
        }
    }

    void turnWaterPlant()
    {
        currentState = waterPlantValue;
        animator.SetInteger("SquareState", 4);

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = waterPlantValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = waterPlantValue;
        }

        StartCoroutine("plantDeath");
        gameObject.tag = "WaterPlant";
    }

    void turnDyingWaterPlant()
    {
        Debug.Log("TurnDyingPlant");
        currentState = dyingWaterPlantValue;

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = dyingWaterPlantValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = dyingWaterPlantValue;
        }
    }

    void turnSunDial()
    {
        currentState = sunDialValue;
        // the sundial uses sprite changes rather than an animation
        animator.enabled = false;
        sr.sprite = sprites[14];
        sundDialCollider.enabled = true;

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = sunDialValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = sunDialValue;
        }
    }

    void turnStandaloneBush()
    {
        currentState = standaloneBushValue;
        animator.SetInteger("SquareState", 8);
        // needs to stop the player and animal so has a seperate collider
        bushCollider.enabled = true;

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = standaloneBushValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = standaloneBushValue;
        }
    }

    void turnEdgeBush()
    {
        currentState = edgeBushValue;
        animator.enabled = false;
        edgeBushCollider.enabled = true;
        // uses the current sprite for the edge of the map this square is on and corners
        if (squareNum == 0)
        {
            sr.sprite = sprites[33];
        }
        else if (squareNum == gridSize-1)
        {
            sr.sprite = sprites[34];
        }
        else if (squareNum == Mathf.Pow(gridSize, 2) - gridSize)
        {
            sr.sprite = sprites[35];
        }
        else if (squareNum == Mathf.Pow(gridSize, 2) - 1)
        {
            sr.sprite = sprites[36];
        }
        else if (squareNum < gridSize)
        {
            if (Random.Range(0,2) == 0)
            {
                sr.sprite = sprites[24];
            }
            else
            {
                sr.sprite = sprites[25];
            }
            edgeBushCollider.offset = new Vector2(0,0.2f);
        }
        else if (squareNum % gridSize == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                sr.sprite = sprites[26];
            }
            else
            {
                sr.sprite = sprites[27];
            }
            edgeBushCollider.direction = 0;
            edgeBushCollider.offset = new Vector2(-0.2f, 0);
            edgeBushCollider.size = new Vector2(0.6f, 1);
        }
        else if ((squareNum + 1) % gridSize == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                sr.sprite = sprites[28];
            }
            else
            {
                sr.sprite = sprites[29];
            }
            edgeBushCollider.direction = 0;
            edgeBushCollider.offset = new Vector2(0.2f, 0);
            edgeBushCollider.size = new Vector2(0.6f, 1);
        }
        else if (squareNum >= Mathf.Pow(gridSize, 2) - gridSize)
        {
            if (Random.Range(0, 2) == 0)
            {
                sr.sprite = sprites[22];
            }
            else
            {
                sr.sprite = sprites[23];
            }
            edgeBushCollider.offset = new Vector2(0, -0.2f);
        }


        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = edgeBushValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = edgeBushValue;
        }
    }

    void turnCattleGrid()
    {
        currentState = cattleGridValue;
        animator.enabled = false;
        sr.sprite = sprites[37];
        // has to collide with animals so removes trigger so collisions occur
        GetComponentInParent<BoxCollider2D>().isTrigger = false;
        gameObject.layer = 8;


        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = cattleGridValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = cattleGridValue;
        }
    }

    void turnDoor()
    {
        currentState = doorValue;
        animator.SetInteger("SquareState", 1);
        // collider must be smaller to ensure player cant trigger it without moving between areas
        GetComponentInParent<BoxCollider2D>().size = new Vector2(0.5f, 1);
        GetComponentInParent<BoxCollider2D>().offset = new Vector2(0.25f, 0);

        if (inSecondArea == false)
        {
            manager.squaresStates[squareNum] = doorValue;
        }
        else if (inSecondArea == true)
        {
            manager.squaresStatesArea2[squareNum] = doorValue;
        }
    }

    public void eatPlant(string coroutineName)
    {
        StopCoroutine(coroutineName);
        turnOff();
        gameObject.tag = "Square";
    }

    public void eatOrPickUpGrass()
    {
        turnOff();
    }

    public void putDownGrass()
    {
        turnOn();
    }


    private IEnumerator checkState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            neighboursStateTotal = 0;
            // become grass if enough neighbours are grass
            if (currentState != waterValue && currentState != waterPlantValue && currentState != dyingWaterPlantValue && currentState != sunDialValue && currentState != standaloneBushValue && currentState != doorValue)
            {
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighboursStates[i] == 1)
                    {
                        neighboursStateTotal += 1;
                    }
                }

                if (neighboursStateTotal >= 2)
                {
                    turnOn();
                }

                // checks that the plant only spreads next to water and next to previous plants        
                bool nextToWater = false;
                bool nextToDyingPlant = false;
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighboursStates[i] == waterValue)
                    {
                        nextToWater = true;
                    }

                    if (neighboursStates[i] == dyingWaterPlantValue)
                    {
                        nextToDyingPlant = true;
                    }
                }

                // randomly spread waterplant to surrounding squares
                if (nextToWater == true && nextToDyingPlant == true && Random.Range(0, 100) <= 40)
                {
                    turnWaterPlant();
                }
            }

            // changes the waterplant animation have grass if it its neighbours are grass rather than dirt
            if (currentState == waterPlantValue)
            {
                int numOfGrass = 0;
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (neighboursStates[i] == 1)
                    {
                        numOfGrass += 1;
                    }
                }
                if (numOfGrass >= 2)
                {
                    animator.SetInteger("SquareState", 6);
                }
                else
                {
                    animator.SetInteger("SquareState", 4);
                }
            }

            // water turns to dirt if the temperature is high enough starting with the edges
            if (currentState == waterValue)
            {
                if (manager.temperature > 50 && inSecondArea == false)
                {
                    for (int i = 0; i < neighbours.Count; i++)
                    {
                        if (neighboursStates[i] == 0 || neighboursStates[i] == 1)
                        {
                            turnOff();
                        }
                    }
                }
                
            }
            

            

            
            yield return new WaitForSeconds(updatePeriod);
        }
    }

    // controls when the plant can spread then die
    private IEnumerator plantDeath()
    {
        yield return new WaitForSeconds(WaterPlantLifeLength);
        turnDyingWaterPlant();
        yield return new WaitForSeconds(2);
        turnOff();
        gameObject.tag = "Square";
        yield break;
    }

   
    // use the gridsize and the squares number to find the squares touching it
    private void findNeighbours()
    {
        // top row
        if (!(squareNum < gridSize))
        {
            if (squareNum % gridSize != 0)
            {
                neighbours.Add(squareNum - (gridSize + 1));
            }
            neighbours.Add(squareNum - gridSize);
            if ((squareNum + 1) % gridSize != 0)
            {
                neighbours.Add(squareNum - (gridSize - 1));
            }
        }

        // left side
        if (squareNum % gridSize != 0)
        {
            neighbours.Add(squareNum - 1);
        }

        // right side
        if ((squareNum + 1) % gridSize != 0)
        {
            neighbours.Add(squareNum + 1);
        }

        // bottom row squares
        if (!(squareNum >= Mathf.Pow(gridSize, 2) - gridSize))
        {
            if (squareNum % gridSize != 0)
            {
                neighbours.Add(squareNum + (gridSize - 1));
            }
            neighbours.Add(squareNum + gridSize);
            if ((squareNum + 1) % gridSize != 0)
            {
                neighbours.Add(squareNum + (gridSize + 1));
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // hide the previous area when moving past the door to the other one
        if (currentState == doorValue)
        {
            if (other.gameObject.tag == "Player")
            {
                manager.changingLevel = true;
            }
        }
    }


}


