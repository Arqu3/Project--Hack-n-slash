using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    int baseCost;
    int[] scalingCost = new int[2];
    int[] totalCost = new int[2];

    GameObject player;
    GameObject shopCanvas;

    public Button[] buttons;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopCanvas = GameObject.FindGameObjectWithTag("ShopCanvas");
        shopCanvas.SetActive(false);
        baseCost = 5;
	}
	
	void Update () 
    {
        //Sets totalcost
        totalCost[0] = baseCost + scalingCost[0];
        totalCost[1] = baseCost + scalingCost[1];

        //Toggle shop active
        if (IsInShop(player) && Input.GetKeyDown(KeyCode.Tab))
            shopCanvas.SetActive(!shopCanvas.activeSelf);
        else if (!IsInShop(player))
            shopCanvas.SetActive(false);

        //Pauses when in shop
        if (shopCanvas.activeSelf)
            Time.timeScale = 0.00001f;
        else
            Time.timeScale = 1f;

        if (shopCanvas.activeSelf)
        {
            //Sets button text when canvas is active
            buttons[0].GetComponentInChildren<Text>().text = "Health \nCost: " + totalCost[0];
            buttons[1].GetComponentInChildren<Text>().text = "Damage \nCost: " + totalCost[1];
        }
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
