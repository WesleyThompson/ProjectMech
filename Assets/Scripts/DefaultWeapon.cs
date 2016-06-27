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
            playerUIScript = GameObject.Find(GlobalVariables.PlayerUI).GetComponent<PlayerUI>();
            damage = 10;
        }

        protected override void Init()
        {
            base.Init();
            playerUIScript.UpdateAmmo();
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
                playerUIScript.UpdateAmmo();
            }
        }
    }
}
