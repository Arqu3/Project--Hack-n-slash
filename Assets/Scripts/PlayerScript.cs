using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    Vector3 newPosition;
    bool hasReached = true;
    public float duration = 50f;
    float yAxis = 0.7f;

    bool pause = true;

    public LayerMask rayMask;
    RaycastHit hit;
    RaycastHit hit2;

    public float range = 3.0f;
    float hitCD = 0.0f;
    Vector3 fwd;

    public float charge = 0f;
    float damage = 5f;
    float offset = 1.7f;
    float radius = 1.35f;

    public Text healthText;
    public Slider chargeBar;
    public NavMeshAgent myAgent;

    void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }
    
	void Start () 
	{
        newPosition = transform.position;
	}
	
	void Update () 
	{
        if (pause == false)
        {
            if (hitCD > 0)
                hitCD--;
            fwd = transform.TransformDirection(Vector3.forward);

            //Uses raycasthit to detect where the player is clicking and moves to that position
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000, rayMask))
                {
                    LeftClick();

                    if (Input.GetKey(KeyCode.LeftShift))
                        hasReached = true;
                    
                    newPosition = hit.point;
                    newPosition.y = yAxis;

                    Rotate();
                }
            }

            Move();

            Debug.DrawRay(transform.position, fwd * range, Color.red);

            if (Input.GetMouseButton(1))
            {
                RightClickHold();
            }
            if (charge <= 2.1f && Input.GetMouseButtonUp(1))
            {
                RightClickRelease();
            }
            
            chargeBar.value = charge;

            //Text display + position
            healthText.text = "" + hitCD + "\n" + charge;
            healthText.rectTransform.anchoredPosition = new Vector2(-Screen.width / 2 - healthText.rectTransform.rect.x * 1.2f, Screen.height / 2 + healthText.rectTransform.rect.y * 1.2f);
        }
	}

    public void Pause()
    {
        pause = !pause;
    }

    void Rotate()
    {
        //Player rotation relative to mouse click
        Vector3 relative = transform.InverseTransformPoint(hit.point);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
    }

    void LeftClick()
    {
        //Player damage
        if (Physics.Raycast(transform.position, fwd, out hit2, 3))
        {
            if (hit.collider.tag == "Enemy")
            {
                hasReached = true;
                if (hitCD <= 0 && charge <= 0)
                {
                    hitCD = 30;
                    if (hit2.collider.tag == "Enemy")
                    {
                        hit2.collider.SendMessage("ApplyDamage", damage);
                        hit2.collider.renderer.material.color = Color.red;
                        Debug.Log("Dealt: " + damage + " damage");
                    }
                }
            }
            else if (hit.collider.tag != "Enemy")
                hasReached = false;
        }
        else
            hasReached = false;
    }

    void RightClickHold()
    {
        hasReached = true;

        if (charge < 2.0f)
            charge += 0.03f;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, rayMask))
        {
            Rotate();
        }
    }

    void RightClickRelease()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * offset, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy")
            {
                colliders[i].SendMessage("ApplyDamage", damage * charge);
                Debug.Log("Dealt: " + damage * charge + " damage");
            }
        }
        charge = 0;
    }

    void Move()
    {
        //If the player hasn't reached its point, it moves towards it
        if (!hasReached && !Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
        transform.position = Vector3.Lerp(transform.position, newPosition, 1 / (duration * (Vector3.Distance(transform.position, newPosition))));

        //Stop the player movement when point is reached
        else if (!hasReached && Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
            hasReached = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.forward * offset, radius);
    }
}
