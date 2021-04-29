using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Text endGameInfo;

	void Start ()
    {
        if (endGameInfo != null)
        {
            endGameInfo.text = "Surviving Animals = " + PlayerPrefs.GetInt("AnimalsAmount").ToString();
            endGameInfo.text += "\n Surviving Plants = " + PlayerPrefs.GetInt("PlantAmount").ToString();
        }
        
    }
	
	
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
