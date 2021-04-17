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
    public Sprite[] sprites;
    public Animator animator;
    int timeToAdd = 0;
    public CircleCollider2D bushCollider;
    public PolygonCollider2D sundDialCollider;


    void Start()
    {
        manager = GameObject.Find("SquareManager").GetComponent<GridManager>();
        gridSize = manager.gridSize;
        currentState = manager.squaresStates[squareNum];

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

        findNeighbours();

        for (int i = 0; i < neighbours.Count; i++)
        {
            oldNeighboursStates.Add(manager.squaresStates[neighbours[i]]);
            neighboursStates.Add(manager.squaresStates[neighbours[i]]);
            if (neighboursStates[i] == 1)
            {
                neighboursStateTotal += 1;
            }
        }

        if (manager.squaresStates[squareNum] == 1)
        {
            turnOn();
        }

        StartCoroutine(checkState());
        

    }

    void Update()
    {
        

        for (int i = 0; i < neighbours.Count; i++)
        {
            neighboursStates[i] = manager.squaresStates[neighbours[i]];
        }

        if (currentState == sunDialValue)
        {
            //Debug.Log(manager.daylength);
            //Debug.Log(manager.daylength / 8);
            //Debug.Log((manager.daylength / 8) * 2);
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

    void turnOn()
    {
        currentState = 1;
        //sr.color = new Color(0f, 0f, 0f, 1f);
        animator.SetInteger("SquareState", 1);
        //sr.sprite = sprites[6];
        manager.squaresStates[squareNum] = 1;
    }

    void turnOff()
    {
        currentState = 0;
        //sr.color = new Color(255f, 255f, 255f, 1f);
        animator.SetInteger("SquareState", 0);
        //sr.sprite = sprites[4];
        manager.squaresStates[squareNum] = 0;
    }

    void turnWater()
    {
        currentState = waterValue;
        //sr.color = new Color(0f, 0f, 255f, 1f);
        animator.SetInteger("SquareState", 3);
        //sr.sprite = sprites[0];
        manager.squaresStates[squareNum] = waterValue;
    }

    void turnWaterPlant()
    {
        currentState = waterPlantValue;
        //sr.color = new Color(0f, 255f, 0f, 1f);
        animator.SetInteger("SquareState", 4);
        //sr.sprite = sprites[9];
        manager.squaresStates[squareNum] = waterPlantValue;
        StartCoroutine("plantDeath");
        gameObject.tag = "WaterPlant";
    }

    void turnDyingWaterPlant()
    {
        Debug.Log("TurnDyingPlant");
        currentState = dyingWaterPlantValue;
        //sr.color = new Color(0f, 255f, 0f, 1f);
        manager.squaresStates[squareNum] = dyingWaterPlantValue;
    }

    void turnSunDial()
    {
        currentState = sunDialValue;
        //sr.color = new Color(0f, 0f, 0f, 1f);
        animator.enabled = false;
        sr.sprite = sprites[14];
        sundDialCollider.enabled = true;
        manager.squaresStates[squareNum] = sunDialValue;
    }

    void turnStandaloneBush()
    {
        currentState = standaloneBushValue;
        //sr.color = new Color(0f, 0f, 0f, 1f);
        animator.SetInteger("SquareState", 8);
        bushCollider.enabled = true;
        manager.squaresStates[squareNum] = standaloneBushValue;
    }

    public void eatPlant(string coroutineName)
    {
        StopCoroutine(coroutineName);
        turnOff();
        gameObject.tag = "Square";
    }

    private IEnumerator checkState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            neighboursStateTotal = 0;
            if (currentState != waterValue && currentState != waterPlantValue && currentState != dyingWaterPlantValue && currentState != sunDialValue && currentState != standaloneBushValue)
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

                if (nextToWater == true && nextToDyingPlant == true && Random.Range(0, 100) <= 20)
                {
                    turnWaterPlant();
                }
            }

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
            

            

            
            yield return new WaitForSeconds(updatePeriod);
        }
    }

    private IEnumerator plantDeath()
    {
        yield return new WaitForSeconds(30);
        turnDyingWaterPlant();
        yield return new WaitForSeconds(2);
        turnOff();
        gameObject.tag = "Square";
        yield break;
    }

   

    private void findNeighbours()
    {
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

        if (squareNum % gridSize != 0)
        {
            neighbours.Add(squareNum - 1);
        }

        if ((squareNum + 1) % gridSize != 0)
        {
            neighbours.Add(squareNum + 1);
        }


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


}


