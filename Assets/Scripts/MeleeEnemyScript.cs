using UnityEngine;
using System.Collections;

public class MeleeEnemyScript : MonoBehaviour {

    public Transform target;
    public float speed = 100;

	void Start () 
    {
	}
	
	void Update () 
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 1 / (speed * (Vector3.Distance(transform.position, target.position))));
	}
}
