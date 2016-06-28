using UnityEngine;
using System.Collections;
using Common;
using Player;

namespace Weapons
{
    public class DefaultWeapon : Weapon
    {
        private PlayerUI playerUIScript;
        public float temp = 15;
        

        void Awake()
        {
            if (GameObject.Find(GlobalVariables.PlayerUI))
            {
                playerUIScript = GameObject.Find(GlobalVariables.PlayerUI).GetComponent<PlayerUI>();
            }
            else
            {
                playerUIScript = null;
                print("No Player UI");
            }
            damage = 10;
        }

        protected override void Init()
        {
            base.Init();
            if (playerUIScript != null)
            {
                playerUIScript.UpdateAmmo();
            }
        }

        void Start()
        {
            Init();
        }
        
        void Update()
        {
            if(Input.GetMouseButtonDown(0) && CanShoot())
            {
                Shoot();
                if (playerUIScript != null)
                {
                    playerUIScript.UpdateAmmo();
                }
            }
        }
    }
}
