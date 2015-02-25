using UnityEngine;
using System.Collections;

public class LookAtTheCamera : MonoBehaviour {

    public Transform targetCamera;

	void Start () 
    {
        targetCamera = GameObject.Find("Player Camera").transform;
	}
	
	void Update () 
    {
        transform.LookAt(targetCamera);
	}
}
