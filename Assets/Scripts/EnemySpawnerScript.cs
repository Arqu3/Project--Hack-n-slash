using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject meleePrefab;
    float timer = 0f;
    GameObject[] getCount;
    float count;


	void Start () 
    {
	}
	
	void Update () 
    {
        getCount = GameObject.FindGameObjectsWithTag("Enemy");
        count = getCount.Length;
        

        if (timer > 0)
            timer--;
        if (timer <= 0 && count <= 10)
        {
            timer = 50f;
            GameObject enemy1 = (GameObject)Instantiate(meleePrefab, new Vector3(Random.Range(-20.0f, 20.0f), 1, Random.Range(-20.0f, 20.0f)), Quaternion.identity);
        }
	}
}
