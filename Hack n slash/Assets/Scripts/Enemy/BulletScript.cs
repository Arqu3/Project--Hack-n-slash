using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void Start () 
	{
	}
	
	void Update () 
	{
        //If the bullet would not hit anything for x sec, remove it
		Destroy(gameObject, 4);
	}
	void OnCollisionEnter(Collision col)
	{
        //Removes bullet on impact
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Wall")
			Destroy(gameObject);
        //If the bullet hit the player, deal damage
		if (col.gameObject.tag == "Player")
			col.gameObject.SendMessage("TakeDamage", 8, SendMessageOptions.DontRequireReceiver);
	}
}
