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
    public float timeScale;
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
        Time.timeScale = timeScale;
        squaresStates = new int[(int)Mathf.Pow(gridSize, 2)];

        counter = 0;
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




        squaresStatesArea2 = new int[(int)Mathf.Pow(gridSize, 2)];

        

        createEdges(squaresStatesArea2);

        //squaresStatesArea2[27] = 1;
        //squaresStatesArea2[28] = 1;
        //squaresStatesArea2[30] = 1;

        //for (int i = 0; i < 15; i++)
        //{
        //    squaresStatesArea2[14 + (gridSize * i)] = 3;
        //    squaresStatesArea2[15 + (gridSize * i)] = 3;
        //    squaresStatesArea2[16 + (gridSize * i)] = 3;
        //}

        
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

        //squaresStatesArea2[50] = 10;
        //squaresStatesArea2[85] = 7;
        squaresStatesArea2[105] = 8;
        squaresStatesArea2[40] = 1;

        //switchStates();
        loadArea2();
        //switchStates();

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

        if (changingLevel == true)
        {
            switchHiddenArea();
        }
    }

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

    void loadArea2()
    {
        int counter2 = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                squaresArea2.Add(Instantiate(squarePrefab, new Vector3(j + gridSize, -i, 0), Quaternion.identity));
                squaresArea2[counter2].GetComponent<SquareControl>().squareNum = counter2;
                //squaresArea2[counter2].SetActive(false);
                //squaresArea2[counter].GetComponent<SpriteRenderer>().enabled = false;
                squaresArea2[counter2].GetComponent<SquareControl>().inSecondArea = true;
                counter2++;
                
            }
        }
    }

    void switchStates()
    {
        int[] squaresStatesTemp;


        squaresStatesTemp = squaresStates;

        squaresStates = squaresStatesArea2;
        squaresStatesArea2 = squaresStatesTemp;

        
    }

    void switchAreas()
    {
        Debug.Log("Switch areas");
        if (loadedArea == 1)
        {
            for (int i = 0; i < squares.Count; i++)
            {
                //squares[i].SetActive(false);
                squares[i].GetComponent<SpriteRenderer>().enabled = false;
                //squaresArea2[i].SetActive(true);
                squaresArea2[i].GetComponent<SpriteRenderer>().enabled = true;
                loadedArea = 2;
            }
        }
        else if (loadedArea == 2)
        {
            for (int i = 0; i < squares.Count; i++)
            {
                //squares[i].SetActive(true);
                squares[i].GetComponent<SpriteRenderer>().enabled = true;
                //squaresArea2[i].SetActive(false);
                squaresArea2[i].GetComponent<SpriteRenderer>().enabled = false;
                loadedArea = 1;
            }
        }

        changingLevel = false;
    }

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
