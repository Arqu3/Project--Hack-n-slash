using UnityEngine;
using System.Collections;

public class GenerateBlockScript : MonoBehaviour {

    GameObject[] blocks;

	void Start () 
    {
        for (int i = 0; i < 10000; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(new Vector3(Random.Range(-201, 201), -8, Random.Range(-201, 201)), 2);
            if(colliders.Length > 0)
            {

            }
        }
	}
	
	void Update () 
    {
	
	}
}
