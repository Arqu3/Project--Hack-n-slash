using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeEnemyScript : Handler {

    public Slider healthSlider;

    float timer;
    RaycastHit hit1;
    Vector3 fwd;

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
        timer = 0.0f;
	}
	
	void Update() 
    {
        fwd = transform.forward;
        Debug.DrawRay(myTransform.position, fwd * 3);
        //Behavior code
        switch (currentState)
        {
            case State.Idle:
                Stop();
                break;

            case State.Attacking:
                MoveTowards(target);
                Attack();
                break;

            case State.Searching:
                Roam(mainFloor);
                break;
        }

        if (timer > 0)
            timer -= 60 * Time.deltaTime;

        //Behavior switching
        if (IsInRangeOf(target, 10f))
            currentState = State.Attacking;

        if (IsOn(spawnFloor))
            MoveTowards(mainFloor);

        if (IsOn(mainFloor) && !IsInRangeOf(target, 10f))
            currentState = State.Searching;
        
        //Health
        healthSlider.value = health;
        if (health <= 0)
        {
            SpawnDrop();
            Handler.Remove(gameObject, 10);
        }

	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }
    void Attack()
    {
        if (Physics.Raycast(myTransform.position, fwd, out hit1, 3))
            if (timer <= 0)
            {
                if (hit1.collider.tag == "Player")
                {
                    timer = 60;
                    hit1.collider.SendMessage("TakeDamage", 5);
                    Debug.Log("attacked");
                }
            }
    }
}
