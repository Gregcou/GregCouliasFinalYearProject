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

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GridManager>();
        gridSize = manager.gridSize;
        currentState = manager.squaresStates[squareNum];

        if (currentState == waterValue)
        {
            turnWater();
        }
        else if (currentState == 4)
        {
            turnWaterPlant();
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

    }

    void turnOn()
    {
        currentState = 1;
        sr.color = new Color(0f, 0f, 0f, 1f);
        manager.squaresStates[squareNum] = 1;
    }

    void turnOff()
    {
        currentState = 0;
        sr.color = new Color(255f, 255f, 255f, 1f);
        manager.squaresStates[squareNum] = 0;
    }

    void turnWater()
    {
        currentState = waterValue;
        sr.color = new Color(0f, 0f, 255f, 1f);
        manager.squaresStates[squareNum] = waterValue;
    }

    void turnWaterPlant()
    {
        currentState = waterPlantValue;
        sr.color = new Color(0f, 255f, 0f, 1f);
        manager.squaresStates[squareNum] = waterPlantValue;
        StartCoroutine("plantDeath");
        gameObject.tag = "WaterPlant";
    }

    void turnDyingWaterPlant()
    {
        Debug.Log("TurnDyingPlant");
        currentState = dyingWaterPlantValue;
        sr.color = new Color(0f, 255f, 0f, 1f);
        manager.squaresStates[squareNum] = dyingWaterPlantValue;
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
            if (currentState != waterValue && currentState != waterPlantValue && currentState != dyingWaterPlantValue)
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


