using UnityEngine;
using System.Collections;

public class DestructableObject : MonoBehaviour {
	[Tooltip("The tag of the object you want to all to destroy this object")]
	[SerializeField] private string destructorTag;
	[Tooltip("The prefab you want to replace this object with on destruction")]
	[SerializeField] private GameObject destroyedObject;

	void OnCollisionEnter (Collision collision){
		if (collision.gameObject.tag == destructorTag) 
		{
			Instantiate (destroyedObject, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
