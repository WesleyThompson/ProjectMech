using UnityEngine;
using System.Collections;

public class DestructableObject : MonoBehaviour {
	[Tooltip("The tag of the object you want to all to destroy this object")]
	public string destructorTag;
	[Tooltip("The prefab you want to replace this object with on destruction")]
	public GameObject debris;
	public Material destroyedMaterial;
	private Component[] colliders;
	public float explosionForce;
	public float explosionRadius;
	public float upwardsModifier;

	private bool hasBeenDestroyed;

	void Start(){
		hasBeenDestroyed = false;
	}

	void OnCollisionEnter (Collision collision){
		if (collision.gameObject.tag == destructorTag && !hasBeenDestroyed) 
		{
			hasBeenDestroyed = true;
			GetComponent<Renderer> ().material = destroyedMaterial;

			//Shutup I know. It works tho.
			Instantiate (debris, transform.position, transform.rotation);
			Instantiate (debris, transform.position, transform.rotation);
		}
	}
}
