using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool paused = false;
    public GameObject pauseMenuUI;
    public Player playerScript;

	// Use this for initialization
	void Start ()
    {
        playerScript = GameObject.Find("Ore").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                pause();
                playerScript.moveable = false;

            }
            else if(paused == true)
            {
                resume();
                playerScript.moveable = true;
            }
        }
	}

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        playerScript.moveable = true;

    }
}
