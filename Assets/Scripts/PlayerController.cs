using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	public float rotationSpeed = 25F;

	private Rigidbody rb;

	public GameObject tankTurret;

	void Update()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector (transform.forward);
		if (Input.GetKey (KeyCode.W))
		{
			transform.Translate (localForward * speed);
		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.Rotate (0, -1  * Time.deltaTime * rotationSpeed, 0);
			tankTurret.transform.Rotate (0, 0, 1 * Time.deltaTime * rotationSpeed);
		} 
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (0, 1 * Time.deltaTime * rotationSpeed, 0);
			tankTurret.transform.Rotate (0, 0, -1 * Time.deltaTime * rotationSpeed);
		}
		if (Input.GetKey (KeyCode.S))
		{
			transform.Translate (localForward * -speed);
		}
	}
}
