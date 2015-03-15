using UnityEngine;
using System.Collections;

public class Handler : MonoBehaviour {

    protected Vector3 down;
    RaycastHit hit;

    //Inherited variables
    protected GameObject mainFloor;
    protected GameObject spawnFloor;
    protected Transform myTransform;
    protected NavMeshAgent myAgent;
    protected Vector3 newPosition;
    protected GameObject target;
    public float health;
    protected float colorTimer;

    public GameObject myGoldPrefab;
    public GameObject myHealthPrefab;

    void Start()
    {
    }
    void Update()
    {
    }

    protected static void Remove(GameObject obj, int score)
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

    protected bool IsInRangeOf(GameObject target, float detectRange)
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

    protected void RotateTowards(GameObject target, float range)
    {
        //Rotates to given target
        float distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        if (distance <= range)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
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

    protected void FlickerColor(Color baseColor)
    {
        //Flickers color when taking damage
        if (colorTimer >= 0)
        {
            colorTimer -= 10.0f * Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (colorTimer <= 0)
            GetComponent<Renderer>().material.color = baseColor;
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
        colorTimer = 0.0f;
    }

    protected void SpawnDrop()
    {
        if (Random.Range(0, 3) == 2)
            Instantiate(myHealthPrefab, transform.position, Quaternion.identity);

        if (Random.Range(0, 4) == 3)
            Instantiate(myGoldPrefab, transform.position, Quaternion.identity);
    }

}
