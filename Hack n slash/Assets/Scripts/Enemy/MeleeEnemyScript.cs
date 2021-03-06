﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeEnemyScript : Handler {

    public Slider healthSlider;

    float attackTimer;
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
        attackTimer = 0.0f;
	}
	
	void Update() 
    {
        FlickerColor(Color.grey);
        fwd = transform.forward;
        Debug.DrawRay(myTransform.position, fwd * 2);
        //Behavior code
        switch (currentState)
        {
            case State.Idle:
                break;

            case State.Attacking:
                MoveTowards(target);
                RotateTowards(target, 10.0f);
                Attack();
                break;

            case State.Searching:
                Roam(mainFloor);
                break;
        }

        if (attackTimer > 0)
            attackTimer -= 60 * Time.deltaTime;

        //Behavior switching
        if (IsInRangeOf(target, 10.0f))
            currentState = State.Attacking;

        if (IsOn(spawnFloor))
            MoveTowards(mainFloor);

        if (IsOn(mainFloor) && !IsInRangeOf(target, 10.0f))
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
        colorTimer = 1.5f;
        health -= damage;
    }
    void Attack()
    {
        if (Physics.Raycast(myTransform.position, fwd, out hit1, 2))
            if (attackTimer <= 0)
            {
                if (hit1.collider.tag == "Player")
                {
                    attackTimer = 60;
                    hit1.collider.SendMessage("TakeDamage", 5);
                }
            }
    }
}
