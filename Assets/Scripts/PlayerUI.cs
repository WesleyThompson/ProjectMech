using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;
using Weapons;

namespace Player
{
    public class PlayerUI : GameBehavior
    {
        public Image healthImg;
        public Image energyImg;
        public Text currAmmoText;
        public Text maxAmmoText;

        private float uiSpeed = 20f;
        
        protected float healthUI;
        public float energyUI;
        public const float MAX_FILL_AMOUNT = .25f;

        private Health playerHealthScript;
        private Energy playerEnergyScript;
        private Weapon weaponScript;
        private float temp=0;

        void Awake()
        {
            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();
            weaponScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Weapon>();
            healthUI = playerHealthScript.GetHealth();
            energyUI = playerEnergyScript.GetEnergy();
        }

        void Update()
        {
            SmoothHealthChange();
            SmoothEnergyChange();
        }


        private void SmoothHealthChange()
        {
            if (playerHealthScript && healthUI != playerHealthScript.GetTargetHealth())
            {
                float distCovered = Mathf.Log((Time.time - playerHealthScript.GetStartTimeHealth()) * uiSpeed, 1.525f);
                float fracJourney = distCovered / Mathf.Abs(playerHealthScript.GetLastTargetHealth()- playerHealthScript.GetTargetHealth());
                healthUI = Mathf.Lerp(playerHealthScript.GetLastTargetHealth(), playerHealthScript.GetTargetHealth(), fracJourney);
                UpdateHealthBarImg();
            }
        }

        private void SmoothEnergyChange()
        {
            if (playerEnergyScript && energyUI != playerEnergyScript.GetTargetEnergy())
            {
                float baseVal = 10f;
                float distCovered = Mathf.Log((Time.time - playerEnergyScript.GetStartTimeEnergy() + (1 / uiSpeed)) * uiSpeed, baseVal);
                float fracJourney = distCovered / 1f;//Mathf.Abs(playerEnergyScript.GetLastTargetEnergy()- playerEnergyScript.GetTargetEnergy());
                energyUI = Mathf.Lerp(playerEnergyScript.GetLastTargetEnergy(), playerEnergyScript.GetTargetEnergy(), fracJourney);
                UpdateEnergyBarImg();
                temp = distCovered;
                print(Time.time - playerEnergyScript.GetStartTimeEnergy());
            }
        }

        public void UpdateHealthBarImg()
        {
            healthImg.fillAmount = healthUI * MAX_FILL_AMOUNT / playerHealthScript.GetMaxHealth();
        }

        public void UpdateEnergyBarImg()
        {
            energyImg.fillAmount = energyUI * MAX_FILL_AMOUNT / playerEnergyScript.GetMaxEnergy();
        }

        public void UpdateAmmo()
        {
            currAmmoText.text = weaponScript.GetCurrAmmo().ToString();
            maxAmmoText.text = weaponScript.GetMaxAmmo().ToString();
        }
    }
}
