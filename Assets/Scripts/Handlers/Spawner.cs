using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject meleePrefab;
    public GameObject rangedPrefab;
    float timer = 0f;
    GameObject[] meleeList;
    GameObject[] rangedList;
    float countM;
    float countR;
    Vector3 spawnPoint;
    GameObject[] spawnPoints;
    float countSP;

	void Start () 
    {
        //Sets relative spawnpoint for enemies
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnFloor").transform.position;
	}
	
	void Update () 
    {
        //Count melee enemies
        meleeList = GameObject.FindGameObjectsWithTag("Enemy1");
        countM = meleeList.Length;

        //Count ranged enemies
        rangedList = GameObject.FindGameObjectsWithTag("Enemy2");
        countR = rangedList.Length;

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnFloor");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (countM <= 1)
                AddEnemy(meleePrefab, spawnPoints[i].transform.position);

            if (countR <= 1)
                AddEnemy(rangedPrefab, spawnPoints[i].transform.position);
        }

        if (timer > 0)
            timer -= 100 * Time.deltaTime;  
        
	}

    void AddEnemy(GameObject prefab, Vector3 relativePosition)
    {
        timer = 50f;
        Instantiate(prefab, new Vector3(Random.Range(relativePosition.x - 2, relativePosition.x + 2), 0.7f, Random.Range(relativePosition.z - 3, relativePosition.z + 3)), Quaternion.identity);
    }
}
