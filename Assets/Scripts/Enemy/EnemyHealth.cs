using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float maxHealth;
    private float currHealth;

	void Awake()
    {
        currHealth = maxHealth;
    }
	
    public void TakeDamage(float dmg)
    {
        currHealth -= dmg;
        if(currHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        //TODO: Kill This
    }
}
