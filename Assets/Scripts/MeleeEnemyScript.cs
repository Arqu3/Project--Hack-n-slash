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

    float health;

    public Slider healthSlider;

    void Awake()
    {
        health = 100;
        myTransform = transform;
    }

	void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update() 
    {
        Movement();

        ConstraintPhysics();

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
        //Enemy rotation relative to player position
        float distance = Vector3.Distance(myTransform.position, target.position);
        if (distance <= range2 && distance >= range)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            hasTarget = true;
        }
        //Move towards player
        else if (distance <= range && distance > stop)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * speed * Time.deltaTime;
            hasTarget = true;
        }
        //Stops when close to player
        else if (distance <= stop)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            hasTarget = true;
        }

        if (distance >= range)
            hasTarget = false;
    }

    void ConstraintPhysics()
    {
        //Sets physics constraints depending on hasTarget
        if (hasTarget == false)
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        else if (hasTarget == true)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
