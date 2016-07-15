using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private float rotationSpeed = 1.5F;

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
			// no acceleration, moves regardless of surfaces (can go through walls)
			//transform.Translate (localForward * speed);

			//moves with regards to surfaces (can't go through walls)
			//rb.AddForce(localForward * speed);

			// moves with regards to surfaces with respect to local, rather than global axis
			rb.AddRelativeForce (localForward * speed);
		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.Rotate (new Vector3(0, -1, 0 * Time.deltaTime * rotationSpeed));

		} 
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (new Vector3(0, 1, 0 * Time.deltaTime * rotationSpeed));
		}
		if (Input.GetKey (KeyCode.S))
		{
			rb.AddRelativeForce (localForward * -speed);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		//Destroy (other.gameObject);
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
		}
	}
}
