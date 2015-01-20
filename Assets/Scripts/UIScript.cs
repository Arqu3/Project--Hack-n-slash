using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

	void Start () 
    {
        camera1.enabled = true;
        camera2.enabled = false;
	}
	
	void Update () 
    {
	}

    public void Switch()
    {
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }
}
