using UnityEngine;
using System.Collections;
using Common;

namespace Weapons
{
    public class Weapon : GameBehavior
    {
        protected float damage = 30;
        protected float fireRate = .1f;
        protected float maxAmmo = 30;
        protected float currAmmo;

        protected virtual void Init()
        {
            currAmmo = maxAmmo;
        }

        protected void Reload()
        {
            currAmmo = maxAmmo;
        }

        protected bool CanShoot()
        {
            return currAmmo > 0 && !ManageGameState.isPaused;
        }

        protected void Shoot()
        {
            currAmmo--;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetFireRate()
        {
            return fireRate;
        }

        public float GetMaxAmmo()
        {
            return maxAmmo;
        }
        
        public float GetCurrAmmo()
        {
            return currAmmo;
        }
    }
}
