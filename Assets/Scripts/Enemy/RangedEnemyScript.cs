using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemyScript : Handler {

	public Slider healthSlider;

	public GameObject bulletPrefab;
    float timer;

	Vector3 fwd;
	float range;
	public LayerMask layerMask;
    RaycastHit hit1;

	enum State
	{
		Idle,
		Searching,
		Attacking
	}
	State currentState;

	void Awake()
	{
		currentState = State.Idle;
        SetValues();
	}

	void Start() 
	{
        range = 10.0f;
        timer = 0.0f;
	}
	
	void Update() 
	{

		fwd = transform.forward;
		Debug.DrawRay(transform.position, fwd * range);

		//Behavior code
		switch (currentState)
		{
			case State.Idle:
				Stop();
				break;

			case State.Attacking:
                MoveTowards(target);
                if (IsInShootRangeOf(target))
                {
                    RotateTowards(target);
                    Stop();
                }

                if (CanSee(target))
                    Shoot(bulletPrefab);
				break;

			case State.Searching:
				Roam(mainFloor);
				break;
		}

		//Shoot timer
		if (timer > 0)
			timer -= 60 * Time.deltaTime;

        //Behavior switching
        if (IsOn(spawnFloor))
            MoveTowards(mainFloor);

        if (IsOn(mainFloor) && !IsInRangeOf(target))
            currentState = State.Searching;

        if (IsInRangeOf(target))
            currentState = State.Attacking;

        //Health
        healthSlider.value = health;
        if (health <= 0)
        {
            SpawnDrop();
            Handler.Remove(gameObject, 15);
        }
	}
	void ApplyDamage(float damage)
	{
		health -= damage;
	}

	bool CanSee(GameObject target)
	{
		//Checks if enemy can see player via raycasting
		if (Physics.Raycast(transform.position, fwd, out hit1, range, layerMask))
		{
			if(hit1.collider.tag == target.tag)
				return true;
		}
		return false;  
	}

	void RotateTowards(GameObject target)
	{
		//Rotates to given target
		float distance = Vector3.Distance(transform.position, target.transform.position);
		Vector3 direction = (target.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		if (distance <= 10)
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
	}

	void Shoot(GameObject bulletPrefab)
	{
		//Shoots clone, timer sets cooldown
		GameObject clone;
		if (timer <= 0)
		{
			timer = 45;
			clone = (GameObject)Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
			clone.GetComponent<Rigidbody>().velocity = transform.forward * 750 * Time.deltaTime;
		}
	}
}
