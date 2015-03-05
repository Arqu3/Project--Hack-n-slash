using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    bool pauseGame = false;

    GameObject pauseCanvas;
    GameObject gameCanvas;
    GameObject FPStext;

	void Start() 
    {
        FPStext = GameObject.FindGameObjectWithTag("FPStext");
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        pauseCanvas.SetActive(false);
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        Pause();
        if (Input.GetKeyDown(KeyCode.R))
            ToggleFPS();
	}

    public void Pause()
    {
        pauseGame = !pauseGame;

        if (pauseGame == true)
        {
            //Pauses the game --ENTER ALL SCRIPTS TO DISABLE HERE--
            Time.timeScale = 0.00001f;
            pauseGame = true;
            GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = false;

            //Activate pause canvas
            pauseCanvas.gameObject.SetActive(true);
        }

        if (pauseGame == false)
        {
            //Unpauses the game --MAKE SURE ALL SCRIPTS ARE RE-ENABLED HERE--
            Time.timeScale = 1.0f;
            pauseGame = false;
            GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = true;

            //Deactivate pause canvas
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    public void Loadmenu()
    {
        //Loads menu
        Application.LoadLevel(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnLevelWasLoaded(int level)
    {
        //Checks what level was loaded
        if (level != 0)
        {
            Time.timeScale = 1.0f;
            Debug.Log("Loaded level: " + level);
        }
    }

    void ToggleFPS()
    {
        //Toggle fps display
        FPStext.SetActive(!FPStext.activeSelf);
    }
}
