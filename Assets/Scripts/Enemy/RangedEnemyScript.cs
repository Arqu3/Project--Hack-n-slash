using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemyScript : Handler {

	public Slider healthSlider;

	public GameObject bulletPrefab;
    float shootTimer;

	float range;
	public LayerMask layerMask;
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
		currentState = State.Idle;
        SetValues();
	}

	void Start() 
	{
        range = 10.0f;
        shootTimer = 0.0f;
	}
	
	void Update() 
	{
        FlickerColor(Color.green);
		fwd = transform.forward;
		Debug.DrawRay(transform.position, fwd * range);

		//Behavior code
		switch (currentState)
		{
			case State.Idle:
				break;

			case State.Attacking:
                MoveTowards(target);
                if (IsInShootRangeOf(target))
                {
                    RotateTowards(target, 10.0f);
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
		if (shootTimer > 0)
			shootTimer -= 60 * Time.deltaTime;

        //Behavior switching
        if (IsOn(spawnFloor))
            MoveTowards(mainFloor);

        if (IsOn(mainFloor) && !IsInRangeOf(target, 10.0f))
            currentState = State.Searching;

        if (IsInRangeOf(target, 10.0f))
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
        colorTimer = 1.5f;
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

	void Shoot(GameObject bulletPrefab)
	{
		//Shoots clone, timer sets cooldown
		GameObject clone;
		if (shootTimer <= 0)
		{
			shootTimer = 90;
			clone = (GameObject)Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
			clone.GetComponent<Rigidbody>().velocity = transform.forward * 750 * Time.deltaTime;
		}
	}
}
