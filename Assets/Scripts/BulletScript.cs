using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void Start () 
    {
	
	}
	
	void Update () 
    {
        Destroy(gameObject, 5);
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
            Destroy(gameObject);

        if (col.gameObject.tag == "Player")
            col.gameObject.SendMessage("ApplyDamage", 10, SendMessageOptions.DontRequireReceiver);
    }
}
