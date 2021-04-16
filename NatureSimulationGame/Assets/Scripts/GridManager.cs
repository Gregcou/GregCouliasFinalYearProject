using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridManager : MonoBehaviour
{

    public GameObject squarePrefab;
    List<GameObject> squares = new List<GameObject>();
    public int gridSize;
    public int listLength;
    public int[] squaresStates;
    public float timeScale;
    public float worldTime = 0.0f;
    public int worldTimeSeconds;
    public int daylength;
    int temperature = 15;

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



        StartCoroutine(increaseTemperature());


        squaresStates[Random.Range(0,listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[Random.Range(0, listLength)] = 1;
        squaresStates[17] = 1;
        squaresStates[18] = 1;
        squaresStates[27] = 1;
        squaresStates[28] = 1;
        squaresStates[30] = 1;
        for (int i=0;i<15;i++)
        {
            squaresStates[14 + (gridSize * i)] = 3;
            squaresStates[15 + (gridSize * i)] = 3;
            squaresStates[16 + (gridSize * i)] = 3;
        }
        squaresStates[gridSize * 6 + 17] = 4;
        squaresStates[gridSize * 6 + 13] = 4;
        squaresStates[gridSize * 8 + 17] = 4;
        squaresStates[30] = 1;

        squaresStates[10] = 7;

    }

    void Update()
    {
        worldTime += Time.deltaTime;
        worldTimeSeconds = (int)(worldTime % 60);
        //Debug.Log("Time = " + worldTimeSeconds);
        //Debug.Log("worldtime = " + worldTime);

        if (worldTimeSeconds == daylength * 3)
        {
            endGame();
        }
    }

    private IEnumerator increaseTemperature()
    {
        int tempIncrease = 1;
        while (true)
        {
            yield return new WaitForSeconds(5);
            temperature += tempIncrease;
            tempIncrease += 1;
        }

    }

    void endGame()
    {
        SceneManager.LoadScene("EndScreen");
    }
}
