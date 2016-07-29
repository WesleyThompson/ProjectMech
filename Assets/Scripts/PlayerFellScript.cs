using UnityEngine;
using System.Collections;

public class PlayerFellScript : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Player")) {
			collision.gameObject.transform.position = new Vector3 (0f, 4f, 0f);
			collision.gameObject.transform.rotation = new Quaternion ();
		}
	}
}
