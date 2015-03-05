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

	void Start () 
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnFloor").transform.position;
	}
	
	void Update () 
    {
        meleeList = GameObject.FindGameObjectsWithTag("Enemy1");
        countM = meleeList.Length;

        rangedList = GameObject.FindGameObjectsWithTag("Enemy2");
        countR = rangedList.Length;

        if (timer > 0)
            timer -= 100 * Time.deltaTime;  

        if (countM <= 1)
            AddEnemy(meleePrefab, spawnPoint);

        if (countR <= 1)
            AddEnemy(rangedPrefab, spawnPoint);
        
	}

    void AddEnemy(GameObject prefab, Vector3 relativePosition)
    {
        timer = 50f;
        Instantiate(prefab, new Vector3(Random.Range(relativePosition.x - 2, relativePosition.x + 2), 0.7f, Random.Range(relativePosition.z - 3, relativePosition.z + 3)), Quaternion.identity);
    }
}
