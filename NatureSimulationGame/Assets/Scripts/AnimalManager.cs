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
            animals.Add(Instantiate(turtlePrefab, new Vector3(0, -0, 0), Quaternion.identity));
            animals[counter].GetComponent<AnimalBehavior>().animalNum = counter;
            counter++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
