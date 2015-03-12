using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject meleePrefab;
    public GameObject rangedPrefab;
    float timer = 0f;
    GameObject[] enemyList;
    float countE;
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
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        countE = enemyList.Length;

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnFloor");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (countE <= 1)
            {
                AddEnemy(meleePrefab, spawnPoints[i].transform.position);
                AddEnemy(rangedPrefab, spawnPoints[i].transform.position);
            }
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
