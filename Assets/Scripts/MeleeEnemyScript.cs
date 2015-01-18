using UnityEngine;
using System.Collections;

public class MeleeEnemyScript : MonoBehaviour {

    public Transform target;
    Transform myTransform;
    public float speed = 3;
    float rotationSpeed = 3;
    float range = 10f;
    float range2 = 10f;
    float stop = 1;

    float health = 100f;

    void Awake()
    {
        myTransform = transform;
    }

	void Start () 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () 
    {
        //Enemy rotation relative to player position
        float distance = Vector3.Distance(myTransform.position, target.position);
        if (distance <= range2 && distance >= range)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        }
        //Move towards player
        else if (distance <= range && distance > stop)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * speed * Time.deltaTime;
        }
        //Stops when close to player
        else if (distance <= stop)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        }

        if (health <= 0)
            Destroy(gameObject);
        
	}
    void ApplyDamage(float damage)
    {
        health -= damage;
    }
}
