using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MeleeEnemyScript : MonoBehaviour {

    public Transform target;
    Transform myTransform;
    public float speed = 3;
    float rotationSpeed = 3;
    float range = 10f;
    float range2 = 10f;
    float stop = 1.5f;
    bool hasTarget = false;
    float distance;

    float health;

    public Slider healthSlider;

    public NavMeshAgent myAgent;
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
        Movement();

        healthSlider.value = health;

        if (health <= 0)
            Destroy(gameObject);
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }

    void Movement()
    {
        distance = Vector3.Distance(myTransform.position, target.position);

        if (distance <= range2)
        {
            myAgent.SetDestination(target.position);
        }
        else
            myAgent.SetDestination(myTransform.position);
    }
}
