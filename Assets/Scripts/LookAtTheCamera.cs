using UnityEngine;
using System.Collections;

public class LookAtTheCamera : MonoBehaviour {

    public Transform camera;

	void Start () 
    {
        camera = GameObject.Find("Player Camera").transform;
	}
	
	void Update () 
    {
        transform.LookAt(camera);
	}
}
