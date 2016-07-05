using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    public float fireRate = 1;
    public float damage = 1;
    private float lastTimeShot;
    private bool canShoot;
    private GameObject objBeingShotAt;
    
	void Awake ()
    {
        lastTimeShot = Time.time - fireRate;
        
	}
	
	void Update ()
    {
	    if(objBeingShotAt != null && canShoot && Time.time - lastTimeShot > fireRate)
        {
            lastTimeShot = Time.time;
            print("Take damage");
            //TODO: Player Take damage
        }
	}

    public void SetShoot(bool shoot, GameObject obj)
    {
        canShoot = shoot;
        objBeingShotAt = obj;
    }
}
