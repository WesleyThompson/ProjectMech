using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;

namespace Player
{
    public class Health : GameBehavior
    {
        public float maxHealth = 100;
        private float health;
        private PlayerUI playerUIScript;

        private float lastTargetHealth;
        private float targetHealth;
        private float startTimeHealth;

		private AudioSource bulletHitSound;

		public Texture2D gameOverImage;

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
        }

        void Start()
        {
            SetHealth(60);
            lastTargetHealth = targetHealth = health;

			bulletHitSound = GetComponent<AudioSource> ();
        }
        
        public void TakeDamage(string type, float damage)
        {
            startTimeHealth = Time.time;
            lastTargetHealth = health;
            SetHealth(health - damage);
            targetHealth = health;

			if (type == "bullet") {
				bulletHitSound.Play ();
			}

            if(health <= 0)
            {
                health = 0;
                Dead();
            }
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
            if (playerUIScript != null)
            {
                playerUIScript.UpdateHealthBarImg();
            }
        }

        private void Dead()
        {
            print("You are dead");
			StartCoroutine(ManageGameState.GameOver ());
        }
			
		void OnGUI() {
			if (health <= 0) {
				print ("draw gameover");
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), gameOverImage, ScaleMode.StretchToFill);
			} else {
				//print ("Still alive");
			}
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
