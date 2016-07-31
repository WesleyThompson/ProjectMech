using UnityEngine;
using System.Collections;
using Common;

namespace Player
{
	public class BulletDamage : MonoBehaviour {

		public float damage;
		
		private Rigidbody rb;
		private ObjectPooling poolScript;
		// Use this for initialization
		void Start () {
			rb = GetComponent<Rigidbody> ();
			poolScript = GameObject.Find("BulletPooler").GetComponent<ObjectPooling> ();
		}

		// need to change this to use Alex's pooling stuff
		void OnCollisionEnter(Collision col)
		{
			//rb.isKinematic = true; this could improve efficiency but need to add logic in pooling or reused bullet will not perform properly
			GameObject rootObjectOfCollision = col.transform.root.gameObject;
			if (rootObjectOfCollision.CompareTag ("Player")) {
				Health script = rootObjectOfCollision.GetComponent<Health> ();
				script.TakeDamage ("bullet", damage);
			}
			StartCoroutine(poolScript.ReturnObject (gameObject)); // return to object pooler
		}
	}
}
