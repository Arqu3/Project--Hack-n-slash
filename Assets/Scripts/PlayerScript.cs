using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    public float JumpForce = 8;
	float rotationSpeed = 500f;

    Vector3 newPosition;
    bool hasReached = true;
    public float duration = 50f;
    float yAxis = 0.7f;

    bool pause = true;

    public LayerMask rayMask;
    RaycastHit hit;

    public float range = 3.0f;
    float hitCD = 0;

    public Text healthText;

	void Start () 
	{
        newPosition = transform.position;
	}
	
	void Update () 
	{
        if (pause == false)
        {
            //Uses raycasthit to detect where the player is clicking and moves to that position
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000, rayMask))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        hasReached = true;
                    else
                        hasReached = false;

                    newPosition = hit.point;
                    newPosition.y = yAxis;

                    //Player rotation relative to mouse click
                    Vector3 relative = transform.InverseTransformPoint(hit.point);
                    float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                    transform.Rotate(0, angle, 0);
                }
            }
            //If the player hasn't reached its point, it moves towards it
            if (!hasReached && !Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
                transform.position = Vector3.Lerp(transform.position, newPosition, 1 / (duration * (Vector3.Distance(transform.position, newPosition))));

            //Stop the player movement when point is reached
            else if (!hasReached && Mathf.Approximately(transform.position.magnitude, newPosition.magnitude))
                hasReached = true;


            //Player hit detection + damage
            if (hitCD > 0)
                hitCD--;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit, 3) && Input.GetMouseButton(0) && hitCD <= 0)
            {
                hitCD = 30;
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.SendMessage("ApplyDamage", 5);
                    hit.collider.renderer.material.color = Color.red;
                }   
            }

            Debug.DrawRay(transform.position, fwd * range, Color.red);

            healthText.text = "" + hitCD;

        }
	}

    public void Pause()
    {
        pause = !pause;
    }
}
