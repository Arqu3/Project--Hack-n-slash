﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeEnemyScript : MonoBehaviour {

    public GameObject target;
    Transform myTransform;
    public float detectRange = 10f;
    float distance;

    float health;

    public Slider healthSlider;
    public NavMeshAgent myAgent;

    enum State
    {
        Idle,
        Searching,
        Attacking
    }
    State currentState;

    RaycastHit hit;
    Vector3 down;

    GameObject mainFloor;
    GameObject spawnFloor;

    Vector3 newPosition;

    void Awake()
    {
        currentState = State.Idle;
        health = 100.0f;
        myTransform = transform;
    }

	void Start() 
    {
        mainFloor = GameObject.FindGameObjectWithTag("Floor1");
        spawnFloor = GameObject.FindGameObjectWithTag("SpawnFloor");

        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update() 
    {
        //Ray
        down = Vector3.down;
        Debug.DrawRay(transform.position, down);

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
                //Handler.Roam(myAgent, mainFloor, myTransform, newPosition);
                Roam(mainFloor);
                break;
        }
        //Behavior switching
        if (IsInRangeOf(target))
            currentState = State.Attacking;

        //if (IsOn(spawnFloor))
        if (Handler.IsOn(spawnFloor, myTransform, hit))
            MoveTowards(mainFloor);

        if (Physics.Raycast(transform.position, down, out hit, 1))
            if (hit.collider.tag == mainFloor.tag && !IsInRangeOf(target))
                currentState = State.Searching;

        //Health
        healthSlider.value = health;
        if (health <= 0)
            Destroy(gameObject);
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

    bool IsInRangeOf(GameObject target)
    {
        //Returns true if close enough to player
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= detectRange;
    }

    void MoveTowards(GameObject target)
    {
        myAgent.SetDestination(target.transform.position);
    }

    void Stop()
    {
        myAgent.SetDestination(myTransform.position);
    }

    bool IsOn(GameObject floor)
    {
        //Checks what floor the object is over
        if (Physics.Raycast(transform.position, down, out hit, 1))
        {
            if (hit.collider.tag == floor.tag)
                return true;
        }
        return false;
    }

     void Roam(GameObject area)
    {
        //Roams given area depending on size
        float distance = Vector3.Distance(myTransform.position, newPosition);
        if (distance <= 2.0f)
        {
            newPosition = new Vector3(Random.Range(area.renderer.bounds.min.x, area.renderer.bounds.max.x), 0.7f, Random.Range(area.renderer.bounds.min.z, area.renderer.bounds.max.z));
        }
        myAgent.SetDestination(newPosition);
    }

}
