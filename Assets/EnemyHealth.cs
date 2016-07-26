using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float health;

	void FixedUpdate () {
		if (health <= 0) {
			print ("Player killed me!!!!!!!!");
			Destroy (gameObject);
		}
	}

	public void takeDamage(float dam) {
		health -= dam;
		print ("Enemy health: " + health);
	}
}
