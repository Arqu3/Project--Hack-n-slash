using UnityEngine;
using System.Collections;

public class LookAtTheCamera : MonoBehaviour {

    public Transform camera;

	// Use this for initialization
	void Start () {

        camera = GameObject.Find("Player Camera").transform;
       // transform.localScale *= -1;
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(camera);

	}
}
