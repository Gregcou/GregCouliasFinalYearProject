using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject turtlePrefab;
    public List<GameObject> animals = new List<GameObject>();

    void Start()
    {
        int counter = 0;
        for (int i = 0; i < 2; i++)
        {
            animals.Add(Instantiate(turtlePrefab, new Vector3(i+i*3, -10, 0), Quaternion.identity));
            animals[counter].GetComponent<AnimalBehavior>().animalNum = counter;
            animals[counter].GetComponent<AnimalBehavior>().growthStage = 1;
            counter++;
        }

        animals.Add(Instantiate(turtlePrefab, new Vector3(30, -10, 0), Quaternion.identity));
        animals[animals.Count - 1].GetComponent<AnimalBehavior>().animalNum = animals.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void haveChild(Vector3 animalPosition)
    {
        animals.Add(Instantiate(turtlePrefab, animalPosition, Quaternion.identity));
        animals[animals.Count-1].GetComponent<AnimalBehavior>().animalNum = animals.Count-1;
    }
}
