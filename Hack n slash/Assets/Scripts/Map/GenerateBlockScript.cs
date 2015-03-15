using UnityEngine;
using System.Collections;

public class GenerateBlockScript : MonoBehaviour {

    public GameObject[] blocks;

    int[] length = new int[31];
    int[] width = new int[31];
    int spacing = 6;
    Vector2 middlePos;

    Vector3 spawnPos;

	void Start() 
    {
        //spawnPos = new Vector3(0, -8, 0);
        //for (int i = 0; i < 1000; i++)
        //{
        //    Collider[] colliders = Physics.OverlapSphere(spawnPos, 3);
        //    if (colliders.Length > 0)
        //    {
        //        return;
        //        spawnPos = new Vector3(Random.Range(-201, 201), -8, Random.Range(-201, 201));
        //    }
        //    else
        //        Instantiate(blocks[Random.Range(0, 3)], spawnPos, Quaternion.identity);
        //}
        //Finds center point of the nested loops
        middlePos = new Vector2(length.Length / 2 * spacing, width.Length / 2 * spacing);
        Debug.Log("The center of the grid is: " + middlePos);
        //for (int l = 0; l < length.Length; l++)
        //{
        //    for (int w = 0; w < width.Length; w++)
        //    {
        //        length[l] = l * spacing;
        //        width[w] = w * spacing;
        //        Instantiate(blocks[Random.Range(0, 3)], new Vector3(length[l] - middlePos.x, -8, width[w] - middlePos.y), Quaternion.identity);
        //    }
        //}

        for (int i = 0; i < 1000; i++)
        {
            Instantiate(blocks[Random.Range(0, 3)], new Vector3(Random.Range(-100, 100), -8, Random.Range(-100, 100)), Quaternion.identity);
        }
	}
	
	void Update() 
    {
	}
}
