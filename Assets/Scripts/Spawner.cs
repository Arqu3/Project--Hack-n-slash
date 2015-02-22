using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject meleePrefab;
    public GameObject rangedPrefab;
    float timer = 0f;
    GameObject[] getCount;
    float count;
    Vector3 spawnPoint;

	void Start () 
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
	}
	
	void Update () 
    {
        getCount = GameObject.FindGameObjectsWithTag("Enemy");
        count = getCount.Length;

        if (timer > 0)
            timer -= 100 * Time.deltaTime;  

        if (count <= 5)
        {
            AddEnemy(meleePrefab, spawnPoint);
            AddEnemy(rangedPrefab, spawnPoint);
        }
	}

    void AddEnemy(GameObject prefab, Vector3 relativePosition)
    {
        timer = 50f;
        Instantiate(prefab, new Vector3(Random.Range(relativePosition.x - 2, relativePosition.x + 2), 0.7f, Random.Range(relativePosition.z - 3, relativePosition.z + 3)), Quaternion.identity);
    }
}
