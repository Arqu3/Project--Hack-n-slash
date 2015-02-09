using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemyScript : MonoBehaviour {

    public Transform target;
    Transform myTransform;

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
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }
}
