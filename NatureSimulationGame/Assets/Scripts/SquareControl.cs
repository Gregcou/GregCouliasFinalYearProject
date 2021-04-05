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

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GridManager>();
        gridSize = manager.gridSize;
        currentState = manager.squaresStates[squareNum];

        if (currentState == 3)
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
        currentState = 3;
        sr.color = new Color(0f, 0f, 255f, 1f);
        manager.squaresStates[squareNum] = 3;
    }

    void turnWaterPlant()
    {
        currentState = 4;
        sr.color = new Color(0f, 255f, 0f, 1f);
        manager.squaresStates[squareNum] = 4;
        StartCoroutine(plantDeath());
    }

    void turnDyingWaterPlant()
    {
        Debug.Log("TurnDyingPlant");
        currentState = 5;
        sr.color = new Color(0f, 255f, 0f, 1f);
        manager.squaresStates[squareNum] = 5;
    }

    private IEnumerator checkState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            neighboursStateTotal = 0;
            if (currentState != 3 && currentState != 4 && currentState != 5)
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
                    if (neighboursStates[i] == 3)
                    {
                        nextToWater = true;
                    }

                    if (neighboursStates[i] == 5)
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
        yield return new WaitForSeconds(5);
        turnDyingWaterPlant();
        yield return new WaitForSeconds(2);
        turnOff();
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


