using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridManager : MonoBehaviour
{

    public GameObject squarePrefab;
    List<GameObject> squares = new List<GameObject>();
    List<GameObject> squaresArea2 = new List<GameObject>();
    public int gridSize;
    public int[] squaresStates;
    public int[] squaresStatesArea2;
    public float worldTime = 0.0f;
    public int worldTimeSeconds;
    public int daylength;
    public int temperature = 15;
    public AnimalManager animalManager;
    public bool changingLevel = false;
    int loadedArea = 1;
    int counter;
    public GameObject areaHidingSquare;

    void Start()
    {

        StartCoroutine(increaseTemperature());
        squaresStates = new int[(int)Mathf.Pow(gridSize, 2)];

        counter = 0;
        // create a grid of squares to create the first area of the level
        for (int i=0;i<gridSize; i++)
        {
            for (int j=0;j<gridSize; j++)
            {
                squares.Add(Instantiate(squarePrefab, new Vector3(j, -i, 0), Quaternion.identity));
                squares[counter].GetComponent<SquareControl>().squareNum = counter;
                counter++;
            }
        }

        createEdges(squaresStates);

        // set the initial states for the first area squares
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

        squaresStates[50] = 7;
        squaresStates[85] = 8;
        squaresStates[58] = 10;
        squaresStates[59] = 11;

        // set the initial states for the second area squares
        squaresStatesArea2 = new int[(int)Mathf.Pow(gridSize, 2)];

        createEdges(squaresStatesArea2);
        
        for (int i=373;i<379;i++)
        {
            squaresStatesArea2[i] = 3;
        }

        for (int i = 354; i < 359; i++)
        {
            squaresStatesArea2[i] = 3;
        }

        for (int i = 335; i < 339; i++)
        {
            squaresStatesArea2[i] = 3;
        }

        for (int i = 316; i < 319; i++)
        {
            squaresStatesArea2[i] = 3;
        }

        for (int i = 297; i < 299; i++)
        {
            squaresStatesArea2[i] = 3;
        }

        squaresStatesArea2[278] = 3;

        squaresStatesArea2[353] = 4;
        squaresStatesArea2[258] = 4;
        squaresStatesArea2[296] = 4;
        squaresStatesArea2[30] = 1;

        squaresStatesArea2[105] = 8;
        squaresStatesArea2[40] = 1;

        loadArea2();

    }

    void Update()
    {
        // track the current time and end the game after 3 days
        worldTime += Time.deltaTime;
        worldTimeSeconds = (int)(worldTime % 60);

        if (worldTimeSeconds == daylength * 3)
        {
            endGame();
        }

        if (changingLevel == true)
        {
            switchHiddenArea();
        }
    }

    // sets a border of bushes which the player can't walk through around the level
    void createEdges(int[] squaresStates)
    {
        for (int i = 0; i < squaresStates.Length; i++)
        {
            //top row squares
            if (i <= gridSize)
            {
                squaresStates[i] = 9;
            }

            // left side
            if (i % gridSize == 0)
            {
                squaresStates[i] = 9;
            }

            // right side
            if ((i + 1) % gridSize == 0)
            {
                squaresStates[i] = 9;
            }

            // bottom row squares
            if (i >= Mathf.Pow(gridSize, 2) - gridSize)
            {
                squaresStates[i] = 9;
            }
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

    // count and save the number of animals and plants left and load the end screen
    void endGame()
    {
        int amountOfPlants = 0;
        for (int i=0;i<squaresStates.Length;i++)
        {
            if (squaresStates[i] == 4 || squaresStates[i] == 5)
            {
                amountOfPlants++;
            }
        }
        PlayerPrefs.SetInt("AnimalsAmount", animalManager.animals.Count);
        PlayerPrefs.SetInt("PlantAmount", amountOfPlants);
        SceneManager.LoadScene("EndScreen");
    }

    // create the second area
    void loadArea2()
    {
        int counter2 = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                squaresArea2.Add(Instantiate(squarePrefab, new Vector3(j + gridSize, -i, 0), Quaternion.identity));
                squaresArea2[counter2].GetComponent<SquareControl>().squareNum = counter2;
                squaresArea2[counter2].GetComponent<SquareControl>().inSecondArea = true;
                counter2++;
                
            }
        }
    }

    // when walking between areas cover the area the player came from 
    void switchHiddenArea()
    {
        if (loadedArea == 1)
        {
            areaHidingSquare.transform.position = new Vector3(9.5f, -9.5f, 0);
            loadedArea = 2;
        }
        else if (loadedArea == 2)
        {
            areaHidingSquare.transform.position = new Vector3(30, -9.5f, 0);
            loadedArea = 1;
        }

        changingLevel = false;
    }
}
