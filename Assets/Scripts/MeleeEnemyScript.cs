using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeEnemyScript : MonoBehaviour {

    public Transform target;
    Transform myTransform;
    public float detectRange = 10f;
    float distance;

    float health;

    public Slider healthSlider;

    public NavMeshAgent myAgent;
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
        if (IsInRangeOf(target))
            MoveTowards(target);
        else
            Stop();

        healthSlider.value = health;

        if (health <= 0)
            Destroy(gameObject);
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }
    bool IsInRangeOf(Transform target)
    {
        //Returns true if close enough to player
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= detectRange;
    }
    void MoveTowards(Transform target)
    {
        myAgent.SetDestination(target.position);
    }
    void Stop()
    {
        myAgent.SetDestination(myTransform.position);
    }
}
