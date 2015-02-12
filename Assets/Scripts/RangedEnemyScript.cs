using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemyScript : MonoBehaviour {

	public Transform target;
	Transform myTransform;
	float distance;

	public NavMeshAgent myAgent;
	public Slider healthSlider;

	float health;

	public GameObject bulletPrefab;
	float timer = 0.0f;

	void Awake()
	{
		health = 100.0f;
		myTransform = transform;
	}

	void Start() 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;	    
	}
	
	void Update() 
	{
		healthSlider.value = health;

		if (timer > 0)
			timer -= 90 * Time.deltaTime;

		if (health <= 0)
			Destroy(gameObject);

		if (IsInRangeOf(target))
			MoveTowards(target);
		else
		{
			RotateTowards(target);
			Stop();
		}
        if (IsInShootRangeOf(target))
        Shoot(bulletPrefab);
	}
	void ApplyDamage(float damage)
	{
		health -= damage;
	}

	bool IsInRangeOf(Transform target)
	{
		//Checks distance between player and enemy, returns true within given parameters
		float distance = Vector3.Distance(transform.position, target.position);
		return distance >= 5 && distance <= 10;
	}

    bool IsInShootRangeOf(Transform target)
    {
        //Checks if the enemy is close enough to shoot
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= 6;
    }

	void MoveTowards(Transform target)
	{
		myAgent.SetDestination(target.position);
	}

	void Stop()
	{
		myAgent.SetDestination(myTransform.position);
	}

	void RotateTowards(Transform target)
	{
		//Rotates to given target
		float distance = Vector3.Distance(transform.position, target.position);
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		if (distance <= 10)
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
	}

	void Shoot(GameObject bulletPrefab)
	{
		GameObject clone;
        if (timer <= 0)
        {
            timer = 30;
            clone = (GameObject)Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            clone.rigidbody.velocity = transform.forward * 1000 * Time.deltaTime;
        }
	}
}
