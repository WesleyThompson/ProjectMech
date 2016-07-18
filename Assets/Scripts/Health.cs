using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;

namespace Player
{
    public class Health : GameBehavior
    {
        private float maxHealth = 100;
        private float health;
        private PlayerUI playerUIScript;

        private float lastTargetHealth;
        private float targetHealth;
        private float startTimeHealth;

        void Start()
        {
            health = 60;
            lastTargetHealth = targetHealth = health;
        }
        
        public void TakeDamage(float damage)
        {
            startTimeHealth = Time.time;
            lastTargetHealth = health;
            SetHealth(health - damage);
            targetHealth = health;

            if(health <= 0)
            {
                health = 0;
                Dead();
            }
        }

        private void SetHealth(float h)
        {
            health = h;
        }

        private void Dead()
        {
            print("You are dead");
            //Restart
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetLastTargetHealth()
        {
            return lastTargetHealth;
        }

        public float GetTargetHealth()
        {
            return targetHealth;
        }
        
        public float GetStartTimeHealth()
        {
            return startTimeHealth;
        }
    }
}
