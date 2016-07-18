using UnityEngine;
using System.Collections;
using Common;

namespace Enemy
{
    public class EnemyHealth : GameBehavior
    {
        public float maxHealth;
        private float currHealth;

        void Awake()
        {
            currHealth = maxHealth;
        }

        public void TakeDamage(float dmg)
        {
            currHealth -= dmg;
            if (currHealth <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            print("Death");
            SearchObjectPool.GetObject(transform.name).ReturnObject(gameObject);
            //TODO: Kill This
        }
    }
}
