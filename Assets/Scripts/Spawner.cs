using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject meleePrefab;
    public GameObject rangedPrefab;
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
            timer -= 100 * Time.deltaTime;  

        if (timer <= 0 && count <= 10)
        {
            AddEnemy(meleePrefab);
            AddEnemy(rangedPrefab);
        }
	}

    void AddEnemy(GameObject prefab)
    {
        timer = 50f;
        GameObject enemy = (GameObject)Instantiate(prefab, new Vector3(Random.Range(-20.0f, 20.0f), 0.7f, Random.Range(-20.0f, 20.0f)), Quaternion.identity);
    }
}
