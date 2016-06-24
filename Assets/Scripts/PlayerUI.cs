using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;

namespace Player
{
    public class PlayerUI : GameBehavior
    {
        public Image healthImg;
        public Image energyImg;

        protected float healthSpeed = 60;
        protected float healthUI;

        protected float energySpeed = 60;
        protected float energyUI;
        public const float MAX_FILL_AMOUNT = .25f;

        private Health playerHealthScript;
        private Energy playerEnergyScript;

        void Awake()
        {
            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();
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
                float distCovered = Mathf.Log((Time.time - playerHealthScript.GetStartTimeHealth()) * healthSpeed, 1.525f);
                float fracJourney = distCovered / Mathf.Abs(playerHealthScript.GetLastTargetHealth()- playerHealthScript.GetTargetHealth());
                healthUI = Mathf.Lerp(playerHealthScript.GetLastTargetHealth(), playerHealthScript.GetTargetHealth(), fracJourney);
                UpdateHealthBarImg();
            }
        }

        private void SmoothEnergyChange()
        {
            if (playerEnergyScript && energyUI != playerEnergyScript.GetTargetEnergy())
            {
                float distCovered = Mathf.Log((Time.time - playerEnergyScript.GetStartTimeEnergy()) * energySpeed, 1.525f);
                float fracJourney = distCovered / Mathf.Abs(playerEnergyScript.GetLastTargetEnergy()- playerEnergyScript.GetTargetEnergy());
                energyUI = Mathf.Lerp(playerEnergyScript.GetLastTargetEnergy(), playerEnergyScript.GetTargetEnergy(), fracJourney);
                UpdateEnergyBarImg();
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
    }
}
