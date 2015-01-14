using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public float distanceZ = 4.3f;
    public float distanceY = 7.75f;
    public float distanceX = -3.75f;

	void Start () 
	{
	
	}
	
	void Update () 
	{
        transform.position = new Vector3(target.position.x - distanceX, target.position.y + distanceY, target.position.z - distanceZ);
	}
}
