using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    Vector3 newPosition;
    bool hasReached = true;
    public float duration = 50f;
    float yAxis = 0.7f;

    public LayerMask rayMask;
    RaycastHit hit;
    RaycastHit hit2;

    public float range = 3.0f;
    float hitCD = 0.0f;
    float hitCD2 = 0.0f;
    Vector3 fwd;

    float charge = 0f;
    float damage = 5f;
    float offset = 1.7f;
    float radius = 1.35f;

    public Text healthText;
    public Slider chargeBar;
    public Slider cooldown1;
    public Slider cooldown2;
    public Slider healthBar;

    float currentHealth;
    float maxHealth;

	void Start () 
	{
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        newPosition = transform.position;
	}
	
	void Update () 
	{
        //Cooldown decrease
        if (hitCD > 0)
            hitCD -= 60 * Time.deltaTime;
        if (hitCD2 > 0)
            hitCD2 -= 60 * Time.deltaTime;

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

                Rotate();
                newPosition = hit.point;
                newPosition.y = yAxis;
            }
        }

        //if (hasReached == false)
        //    Rotate(); 

        Movement();
        UI();

        if (hitCD2 <= 0)
        {
            if (Input.GetMouseButton(1))
            {
                RightClickHold();
            }
            if (Input.GetMouseButtonUp(1))
            {
                RightClickRelease();
            }
        }

        healthText.text = currentHealth + "/" + maxHealth;

        Debug.DrawRay(transform.position, fwd * range, Color.red);
	}
    void Rotate()
    {
        //Player rotation relative to mouse click
        Vector3 relative = transform.InverseTransformPoint(hit.point);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg * Time.deltaTime * 100;
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
                    //if (hit2.collider.tag == "Enemy")
                    {
                        hit2.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
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
            charge += 3 * Time.deltaTime;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, rayMask))
        {
            Rotate();
        }
    }

    void RightClickRelease()
    {
        //Hits all enemies in a sphere using collider array 
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * offset, radius);
        float totalDamage = damage * charge;
        hitCD2 = 100;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].SendMessage("ApplyDamage", (int)totalDamage, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Dealt: " + (int)totalDamage + " damage");
        }
        charge = 0;
    }

    void Movement()
    {
        //If the player hasn't reached its point, it moves towards it
        //if (!hasReached && !Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
        //transform.position = Vector3.Lerp(transform.position, newPosition, 1 / (duration * (Vector3.Distance(transform.position, newPosition))) * Time.deltaTime);

        //Stop the player movement when point is reached
        //if (!hasReached && Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
        //    hasReached = true;
        
        //if (!hasReached)
        //    rigidbody.velocity = fwd * 2;
        //else
        //    rigidbody.velocity = Vector3.zero;

        if (Vector3.Distance(newPosition, transform.position) > 1.0f)
            rigidbody.velocity = fwd * 5;
        else
            rigidbody.velocity = Vector3.zero;
    }

    void UI()
    {
        //Healthbar
        healthBar.maxValue = 100;
        healthBar.value = currentHealth;

        //Chargebar visibility
        if (charge <= 0)
            chargeBar.gameObject.SetActive(false);
        else
            chargeBar.gameObject.SetActive(true);

        chargeBar.value = charge;

        //First button
        cooldown1.maxValue = 30;
        cooldown1.value = hitCD;
        if (hitCD <= 0)
            cooldown1.fillRect.localScale = Vector3.zero;
        else
            cooldown1.fillRect.localScale = new Vector3(1, 1, 1);

        //Second button
        cooldown2.maxValue = 100;
        cooldown2.value = hitCD2;
        if (hitCD2 <= 0)
            cooldown2.fillRect.localScale = Vector3.zero;
        else
            cooldown2.fillRect.localScale = new Vector3(1, 1, 1);
    }

    void ApplyDamage(float damage)
    {
        currentHealth -= damage;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.forward * offset, radius);
    }
}
