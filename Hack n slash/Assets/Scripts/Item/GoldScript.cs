using UnityEngine;
using System.Collections;

public class GoldScript : MonoBehaviour {

    public int amount;
    public int baseAmount;
    BoxCollider myCollider;

	void Start () 
    {
        myCollider = GetComponent<BoxCollider>();
        baseAmount = 5;
	}
	
	void Update () 
    {

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            amount = Random.Range(0, 10) + baseAmount;
            PlayerScript.gold += amount;
            Destroy(gameObject);
        }
    }
}
