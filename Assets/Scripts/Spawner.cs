using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject meleePrefab;
    float timer = 0f;
    GameObject[] getCount;
    float count;
    bool pause = true;

	void Start () 
    {
	}
	
	void Update () 
    {
        if (pause == false)
        {
            getCount = GameObject.FindGameObjectsWithTag("Enemy");
            count = getCount.Length;

            if (timer > 0)
                timer--;
            if (timer <= 0 && count <= 10)
            {
                timer = 50f;
                GameObject enemy1 = (GameObject)Instantiate(meleePrefab, new Vector3(Random.Range(-20.0f, 20.0f), 0.7f, Random.Range(-20.0f, 20.0f)), Quaternion.identity);
            }
        }
	}

    public void Pause()
    {
        pause = !pause;
    }
}
