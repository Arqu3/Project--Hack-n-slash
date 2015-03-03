using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

    static Vector3 down = Vector3.down;
    //static Vector3 newPosition = Vector3.zero;

    void Start()
    {
    }

    void Update()
    {
    }

    public static void Kill(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.S))
            Destroy(obj);
    }

    public static void Roam(NavMeshAgent myAgent, GameObject area, Transform myTransform, Vector3 newPosition)
    {
        //Roams given area depending on size
        float distance = Vector3.Distance(myTransform.position, newPosition);
        if (distance <= 2.0f)
        {
            newPosition = new Vector3(Random.Range(area.GetComponent<Renderer>().bounds.min.x, area.GetComponent<Renderer>().bounds.max.x), 0.7f, Random.Range(area.GetComponent<Renderer>().bounds.min.z, area.GetComponent<Renderer>().bounds.max.z));
        }
        myAgent.SetDestination(newPosition);
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
