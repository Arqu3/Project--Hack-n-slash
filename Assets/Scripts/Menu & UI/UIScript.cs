using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    GameObject pauseCanvas;
    GameObject gameCanvas;
    GameObject GOCanvas;
    public Text GOText;
    public GameObject FPStext;

    public enum State
    {
        Paused,
        InGame,
        GameOver
    }
    public static State currentState;

	void Start() 
    {
        currentState = State.InGame;
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        GOCanvas = GameObject.FindGameObjectWithTag("GOCanvas");
        pauseCanvas.SetActive(false);
        GOCanvas.SetActive(false);
	}
	
	void Update () 
    {
        //Toggle menu
        if (currentState != State.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentState == State.InGame)
                    currentState = State.Paused;
                else if (currentState == State.Paused)
                    currentState = State.InGame;
            }
        }
        //Game over
        if (PlayerScript.currentHealth <= 0)
            currentState = State.GameOver;
        //Fps toggle
        if (Input.GetKeyDown(KeyCode.R))
            ToggleFPS();
        
        StateHandling();
	}

    public void StateHandling()
    {
        //What happens in each menu state
        switch(currentState)
        {
            case State.InGame:
            //Unpauses the game --MAKE SURE ALL SCRIPTS ARE RE-ENABLED HERE--
            Time.timeScale = 1.0f;
            GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = true;
            //Deactivate pause canvas
            pauseCanvas.gameObject.SetActive(false);
            break;

            case State.Paused:
            //Pauses the game --ENTER ALL SCRIPTS TO DISABLE HERE--
            Time.timeScale = 0.00001f;
            GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = false;
            //Activate pause canvas
            pauseCanvas.gameObject.SetActive(true);
            break;

            case State.GameOver:
            Time.timeScale = 0.00001f;
            GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = false;
            GOCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            GOText.text = ("Game over! \nYour score was: " + ScoreHandlerScript.playerScore);
            break;
        }
    }

    public void SetState()
    {
        //Sets state to ingame via button click in menu
        currentState = State.InGame;
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
        if (level == 1)
        {
            Time.timeScale = 1.0f;
            ScoreHandlerScript.playerScore = 0;
            PlayerScript.damage = 30;
            PlayerScript.currentHealth = 100;
            PlayerScript.maxHealth = 100;
            PlayerScript.gold = 0;
        }
    }

    void ToggleFPS()
    {
        //Toggle fps display
        FPStext.SetActive(!FPStext.activeSelf);
    }
}
