using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
	public Vector3 JumpForce = new Vector3(0, 8, 0);
	float rotationSpeed = 500f;

	public GameObject blockPrefab;
	float blockTimer = 0f;

	public Transform target;
	public float speed;
    Vector3 pos;

	void Start () 
	{
        pos = transform.position;
	}
	
	void Update () 
	{
		//Screen.showCursor = false;

        if (Input.GetMouseButton(1))
        {
            pos = Input.mousePosition;
            pos.z = 45f;
            pos = Camera.main.ScreenToWorldPoint(pos);
        }
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);

        Movement();

        //Rotation();

        Addblocks();
	}
    void Movement()
    {
        float speed = Input.GetAxis("Horizontal") * 10;
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, rigidbody.velocity.z);
        if (Input.GetKey(KeyCode.Space) == true)
        {
            rigidbody.AddForce(JumpForce);
        }
        float step = speed * Time.deltaTime;
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void Rotation()
    {
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX *= Time.deltaTime;
        rotationY *= Time.deltaTime;
        transform.Rotate(rotationY, rotationX, 0);
    }

    void Addblocks()
    {
        if (blockTimer > 0)
            blockTimer--;
        if (Input.GetMouseButton(0) && blockTimer == 0)
        {
            blockTimer = 30;
            GameObject block = (GameObject)Instantiate(blockPrefab, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        }
    }
}
