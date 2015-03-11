using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {

    int baseCost;
    int[] scalingCost = new int[2];
    int[] totalCost = new int[2];

    GameObject player;
    GameObject shopCanvas;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopCanvas = GameObject.FindGameObjectWithTag("ShopCanvas");
        shopCanvas.SetActive(false);
        baseCost = 5;
	}
	
	void Update () 
    {
        totalCost[0] = baseCost + scalingCost[0];
        totalCost[1] = baseCost + scalingCost[1];

        if (IsInShop(player) && Input.GetKeyDown(KeyCode.Tab))
            shopCanvas.SetActive(true);
        else if (!IsInShop(player))
            shopCanvas.SetActive(false);
	}

    public void AddHealth()
    {
        if (PlayerScript.gold >= totalCost[0])
        {
            //Remove current gold
            PlayerScript.gold -= totalCost[0];
            //Increase upgrade cost
            scalingCost[0] += 8;
            //Apply upgrade
            PlayerScript.maxHealth += 5;
            PlayerScript.currentHealth += 5;
        }
    }

    public void AddDamage()
    {
        if (PlayerScript.gold >= totalCost[1])
        {
            //Remove current gold
            PlayerScript.gold -= totalCost[1];
            //Increase upgrade cost
            scalingCost[1] += 15;
            //Apply upgrade
            PlayerScript.damage += 2;
        }
    }

    bool IsInShop(GameObject player)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 3)
            return true;
        else
            return false;
    }
}
