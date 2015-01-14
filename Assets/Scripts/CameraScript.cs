using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target;
    float distanceZ = 4.3f;
    float distanceY = 7.75f;
    float distanceX = -3.75f;

	void Start () 
	{
	
	}
	
	void Update () 
	{
        transform.position = new Vector3(target.position.x - distanceX, target.position.y + distanceY, target.position.z - distanceZ);
	}
}
