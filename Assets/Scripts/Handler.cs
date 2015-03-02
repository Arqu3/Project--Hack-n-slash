using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

    static Vector3 newPosition;
    static Vector3 down = Vector3.down;

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

    public static void Roam(NavMeshAgent myAgent, GameObject area, Transform myTransform)
    {
        //Roams given area depending on size
        float distance = Vector3.Distance(myTransform.position, newPosition);
        if (distance <= 2.0f)
        {
            newPosition = new Vector3(Random.Range(area.renderer.bounds.min.x, area.renderer.bounds.max.x), 0.7f, Random.Range(area.renderer.bounds.min.z, area.renderer.bounds.max.z));
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
