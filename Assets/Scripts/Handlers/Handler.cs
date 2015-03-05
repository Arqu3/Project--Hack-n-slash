using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

    protected Vector3 down;
    protected RaycastHit hit;

    GameObject[] testList;

    //Inherited variables
    protected GameObject mainFloor;
    protected GameObject spawnFloor;
    protected Transform myTransform;
    protected NavMeshAgent myAgent;
    protected Vector3 newPosition;
    protected GameObject target;
    protected float health;
    protected float detectRange;

    public GameObject testPrefab;

    void Start()
    {
    }

    void Update()
    {
        //Test objects for first time handler usage
        testList = GameObject.FindGameObjectsWithTag("Test");

        if (Input.GetKeyDown(KeyCode.A))
            Instantiate(testPrefab);

        for (int i = 0; i < testList.Length; i++)
        {
            if (testList[i].GetComponent<Testscript>().health <= 0)
                Remove(testList[i].gameObject, 10);
        }
    }

    public static void Remove(GameObject obj, int score)
    {
        ScoreHandlerScript.playerScore += score;
        DestroyObject(obj);
    }

    protected virtual void Roam(GameObject area)
    {
        //Roams given area depending on size
        float distance = Vector3.Distance(myTransform.position, newPosition);
        if (distance <= 2.0f)
        {
            newPosition = new Vector3(Random.Range(area.GetComponent<Renderer>().bounds.min.x, area.GetComponent<Renderer>().bounds.max.x), 0.7f, Random.Range(area.GetComponent<Renderer>().bounds.min.z, area.GetComponent<Renderer>().bounds.max.z));
        }
        myAgent.SetDestination(newPosition);
    }

    protected bool IsInRangeOf(GameObject target)
    {
        //Returns true if close enough to player
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= detectRange;
    }

    protected bool IsInShootRangeOf(GameObject target)
    {
        //Checks if the enemy is close enough to shoot
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= 6;
    }

    protected void MoveTowards(GameObject target)
    {
        myAgent.SetDestination(target.transform.position);
    }

    protected void Stop()
    {
        myAgent.SetDestination(myTransform.position);
    }

    protected bool IsOn(GameObject floor)
    {
        //Checks what floor the object is over
        if (Physics.Raycast(transform.position, down, out hit, 1))
        {
            if (hit.collider.tag == floor.tag)
                return true;
        }
        return false;
    }

    protected virtual void SetValues()
    {
        //Sets standard values for all inheriting classes
        mainFloor = GameObject.FindGameObjectWithTag("Floor1");
        spawnFloor = GameObject.FindGameObjectWithTag("SpawnFloor");
        myAgent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        down = Vector3.down;
        newPosition = new Vector3(Random.Range(-20, 20), 0.7f, Random.Range(-20, 20));
        target = GameObject.FindGameObjectWithTag("Player");
        health = 100;
        detectRange = 10.0f;
    }
}
