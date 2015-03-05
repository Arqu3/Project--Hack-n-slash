using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHandlerScript : MonoBehaviour {

    public Text scoreText;
    public static int playerScore;

	void Start() 
    {
	}
	
	void Update() 
    {
        scoreText.text = "Score: " + playerScore;
	}
}
