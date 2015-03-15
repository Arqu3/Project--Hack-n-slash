using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    float amount;

	void Start() 
    {
        amount = PlayerScript.maxHealth * 0.33f;
	}
	
	void Update() 
    {
        
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerScript.currentHealth < PlayerScript.maxHealth)
                PlayerScript.currentHealth += amount;

            Destroy(gameObject);
        }
    }
}
