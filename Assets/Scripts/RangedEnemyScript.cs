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

    RaycastHit hit;
    Vector3 fwd;
    float range;
    public LayerMask layerMask;

	void Awake()
	{
        range = 10.0f;
		health = 100.0f;
		myTransform = transform;
	}

	void Start() 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;	    
	}
	
	void Update() 
	{
        fwd = transform.forward;
        Debug.DrawRay(transform.position, fwd * range);

		healthSlider.value = health;
        //Shoot timer
		if (timer > 0)
			timer -= 60 * Time.deltaTime;

		if (health <= 0)
			Destroy(gameObject);

        //Behavior
		if (IsInRangeOf(target))
			MoveTowards(target);
		else
		{
			RotateTowards(target);
			Stop();
		}

        if (CanSee(target))
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

    bool CanSee(Transform target)
    {
        if (Physics.Raycast(transform.position, fwd, out hit, range, layerMask))
        {
            if(hit.collider.tag == target.tag)
                return true;
        }
        return false;  
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
            timer = 45;
            clone = (GameObject)Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            clone.rigidbody.velocity = transform.forward * 750 * Time.deltaTime;
        }
	}
}
