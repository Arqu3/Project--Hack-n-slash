using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

    bool pauseGame = false;

    public GameObject button;

	void Start () 
    {
        camera1.enabled = true;
        camera2.enabled = false;
	}
	
	void Update () 
    {
        Pause();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CameraSwitch();
            button.SetActive(true);
        }
	}

    public void CameraSwitch()
    {
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }

    public void ToggleButton()
    {
        button.SetActive(false);
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                //Pauses the game --ENTER ALL SCRIPTS TO DISABLE HERE--
                Time.timeScale = 0.00001f;
                pauseGame = true;
                GameObject.Find("Player").GetComponent<PlayerScript>().enabled = false;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    MeleeEnemyScript mel = enemy.GetComponent<MeleeEnemyScript>();
                    if (mel != null)
                        mel.enabled = false;
                }
                GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = false;
            }

            if (pauseGame == false)
            {
                //Unpauses the game --MAKE SURE ALL SCRIPTS ARE RE-ENABLED HERE--
                Time.timeScale = 1f;
                pauseGame = false;
                GameObject.Find("Player").GetComponent<PlayerScript>().enabled = true;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    MeleeEnemyScript mel = enemy.GetComponent<MeleeEnemyScript>();
                    if(mel != null)
                        mel.enabled = true;
           
                }
                GameObject.Find("Enemyspawner").GetComponent<Spawner>().enabled = true;
            }
        }
    }
}
