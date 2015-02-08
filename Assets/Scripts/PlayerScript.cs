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
    
	void Start () 
	{
        newPosition = transform.position;
	}
	
	void Update () 
	{
        //Cooldown decrease
        if (hitCD > 0)
            hitCD--;
        if (hitCD2 > 0)
            hitCD2--;
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

        //Text display + position
        healthText.text = "Leftclick cooldown: " + hitCD + "\n" + "Rightclick cooldown: " + hitCD2;
        healthText.rectTransform.anchoredPosition = new Vector2(-Screen.width / 2 - healthText.rectTransform.rect.x * 1.2f, Screen.height / 2 + healthText.rectTransform.rect.y * 1.2f);

        Debug.DrawRay(transform.position, fwd * range, Color.red);
        
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
        //Hits all enemies in a sphere using collider array 
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * offset, radius);
        float totalDamage = damage * charge;
        hitCD2 = 100;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy")
            {
                colliders[i].SendMessage("ApplyDamage", totalDamage);
                Debug.Log("Dealt: " + totalDamage + " damage");
            }
        }
        charge = 0;
    }

    void Movement()
    {
        //If the player hasn't reached its point, it moves towards it
        if (!hasReached && !Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
        transform.position = Vector3.Lerp(transform.position, newPosition, 1 / (duration * (Vector3.Distance(transform.position, newPosition))));

        //Stop the player movement when point is reached
        else if (!hasReached && Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
            hasReached = true;
    }

    void UI()
    {
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

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.forward * offset, radius);
    }
}
