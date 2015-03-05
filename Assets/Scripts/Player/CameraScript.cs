using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    Transform target;
    public float distanceZ = 6.61f;
    public float distanceY = 10.3f;
    public float distanceX = -5.12f;

	void Start () 
	{
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () 
	{
        transform.position = new Vector3(target.position.x - distanceX, target.position.y + distanceY, target.position.z - distanceZ);
	}
}
