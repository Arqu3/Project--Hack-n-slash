using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

    static Vector3 down = Vector3.down;

    GameObject[] meleeList;
    GameObject[] rangedList;

    void Start()
    {
    }

    void Update()
    {
        meleeList = GameObject.FindGameObjectsWithTag("Enemy1");
        rangedList = GameObject.FindGameObjectsWithTag("Enemy2");
    }

    public static void Remove(GameObject obj, int score)
    {
        ScoreHandlerScript.playerScore += score;
        Destroy(obj);
    }

    public static bool IsOn(GameObject floor, Transform myTransform, RaycastHit hit)
    {
        //Checks what floor the object is over
        if (Physics.Raycast(myTransform.position, down, out hit, 1))
        {
            if (hit.collider.tag == floor.tag)
                return true;
        }
        return false;
    }

}
