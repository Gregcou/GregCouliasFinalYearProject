using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject squarePrefab;
    List<GameObject> squares = new List<GameObject>();
    public int gridSize;
    public int listLength;
    public int[] squaresStates;
    public float timeScale;

    void Start()
    {
        Time.timeScale = timeScale;
        listLength = (int)Mathf.Pow(gridSize, 2);
        squaresStates = new int[listLength];

        int counter = 0;
        for (int i=0;i<gridSize; i++)
        {
            for (int j=0;j<gridSize; j++)
            {
                squares.Add(Instantiate(squarePrefab, new Vector3(j, -i, 0), Quaternion.identity));
                squares[counter].GetComponent<SquareControl>().squareNum = counter;
                counter++;
            }
                
        }
        squaresStates[Random.Range(0,listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[2] = 3;
        squaresStates[3] = 3;
        squaresStates[12] = 3;
        squaresStates[13] = 3;
        squaresStates[22] = 3;
        squaresStates[23] = 3;
        squaresStates[32] = 3;
        squaresStates[33] = 3;
        squaresStates[32] = 3;
        squaresStates[33] = 3;
        squaresStates[42] = 3;
        squaresStates[43] = 3;
        squaresStates[52] = 3;
        squaresStates[53] = 3;
        squaresStates[1] = 4;
        squaresStates[41] = 4;
        squaresStates[24] = 4;
        squaresStates[30] = 1;
    }

    void Update()
    {
        
    }
}
