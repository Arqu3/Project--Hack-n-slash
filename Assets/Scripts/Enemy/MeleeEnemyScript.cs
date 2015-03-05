using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeEnemyScript : Handler {

    public Slider healthSlider;

    enum State
    {
        Idle,
        Searching,
        Attacking
    }
    State currentState;

    void Awake()
    {
        SetValues();
        currentState = State.Idle;
    }

	void Start() 
    {
	}
	
	void Update() 
    {

        //Behavior code
        switch (currentState)
        {
            case State.Idle:
                Stop();
                break;

            case State.Attacking:
                MoveTowards(target);
                break;

            case State.Searching:
                Roam(mainFloor);
                break;
        }
        //Behavior switching
        if (IsInRangeOf(target))
            currentState = State.Attacking;

        if (IsOn(spawnFloor))
            MoveTowards(mainFloor);

        if (IsOn(mainFloor) && !IsInRangeOf(target))
            currentState = State.Searching;
        //Health
        healthSlider.value = health;
        if (health <= 0)
            Handler.Remove(gameObject, 10);
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }
}
