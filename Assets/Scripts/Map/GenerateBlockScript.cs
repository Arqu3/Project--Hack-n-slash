using UnityEngine;
using System.Collections;

public class GenerateBlockScript : MonoBehaviour {

    public GameObject[] blocks;
    Vector3 spawnPos;

	void Start () 
    {
        spawnPos = new Vector3(0, -8, 0);
        for (int i = 0; i < 1000; i++)
        {
            //Collider[] colliders = Physics.OverlapSphere(spawnPos, 3);
            //if (colliders.Length > 0)
            //{
            //    spawnPos = new Vector3(Random.Range(-201, 201), -8, Random.Range(-201, 201));
            //}
            //else
            //    Instantiate(blocks[Random.Range(0, 3)], spawnPos, Quaternion.identity);
            Instantiate(blocks[Random.Range(0, 3)], new Vector3(Random.Range(-151, 151), -8, Random.Range(-151, 151)), Quaternion.identity);
        }
	}
	
	void Update () 
    {
	
	}
}
