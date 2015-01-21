using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	void Start () 
	{
	}
	
	void Update () 
	{
		if (transform.position.y < -10)
			Destroy(gameObject);

        Destroy(gameObject, 3);
	}
}
