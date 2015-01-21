using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

    bool pauseGame = false;

    public Transform button;

	void Start () 
    {
        camera1.enabled = true;
        camera2.enabled = false;
	}
	
	void Update () 
    {
        Pause();
	}

    public void CameraSwitch()
    {
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                //Pauses the game --ENTER ALL SCRIPTS TO DISABLE HERE--
                Time.timeScale = 0.000001f;
                pauseGame = true;
                GameObject.Find("Player").GetComponent<PlayerScript>().enabled = false;
                GameObject.Find("Enemy").GetComponent<MeleeEnemyScript>().enabled = false;
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerScript>().enabled = false;
                button.GetComponent<Button>().interactable = false;
            }

            if (pauseGame == false)
            {
                //Unpauses the game --MAKE SURE ALL SCRIPTS ARE RE-ENABLED HERE--
                Time.timeScale = 1f;
                pauseGame = false;
                GameObject.Find("Player").GetComponent<PlayerScript>().enabled = true;
                GameObject.Find("Enemy").GetComponent<MeleeEnemyScript>().enabled = true;
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawnerScript>().enabled = true;
                button.GetComponent<Button>().interactable = false;
            }
        }
    }
}
