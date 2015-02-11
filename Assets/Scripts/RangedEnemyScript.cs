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

    void Awake()
    {
        health = 100;
        myTransform = transform;
        myAgent = GetComponent<NavMeshAgent>();
    }

	void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;	    
	}
	
	void Update() 
    {
        healthSlider.value = health;

        if (health <= 0)
            Destroy(gameObject);

        if (IsInRangeOf(target))
            MoveTowards(target);
        else
        {
            Stop();
            RotateTowards(target);
        }
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
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }
}
