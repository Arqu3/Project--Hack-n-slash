using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHandlerScript : MonoBehaviour {

    public Text scoreText;
    public static int playerScore;

	void Start() 
    {
        playerScore = 0;
	}
	
	void Update() 
    {
        scoreText.text = "Score: " + playerScore;
	}
}
