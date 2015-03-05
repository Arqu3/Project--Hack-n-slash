using UnityEngine;
using System.Collections;

public class Testscript : Handler {

    enum State
    {
        Attacking,
        Searching
    }

    State currentState;

	void Start () 
    {
        currentState = State.Searching;
        SetValues();
	}
	
	void Update () 
    {
        Roam(mainFloor);
	}
}
