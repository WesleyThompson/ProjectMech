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

        public float xCutOff = 70f;
        public float totalTime = 3f;
        private float uiSpeed = 1;
        private float baseVal = 3f;

        protected float healthUI;
        public float energyUI;
        public const float MAX_FILL_AMOUNT = .25f;

        private Health playerHealthScript;
        private Energy playerEnergyScript;
        private Weapon weaponScript;

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
                healthUI = SmoothUIChange(playerHealthScript.GetStartTimeHealth(), playerHealthScript.GetLastTargetHealth(), playerHealthScript.GetTargetHealth());
                UpdateHealthBarImg();
            }
        }

        private void SmoothEnergyChange()
        {
            if (playerEnergyScript && energyUI != playerEnergyScript.GetTargetEnergy())
            {
                energyUI = SmoothUIChange(playerEnergyScript.GetStartTimeEnergy(), playerEnergyScript.GetLastTargetEnergy(), playerEnergyScript.GetTargetEnergy());
                UpdateEnergyBarImg();
            }
        }

        private float GetSmoothLog(float xPos)
        {
            return Mathf.Log((xPos + (1 / uiSpeed)) * uiSpeed, baseVal);
        }

        private float SmoothUIChange(float startTime, float lastTarget, float target)
        {
            //Max Y
            float yCutOff = GetSmoothLog(xCutOff);
            float elapsedTime = Time.time - startTime;

            //Converts the time percentage into the function x percentage
            float xVal = xCutOff * elapsedTime / totalTime;

            //Get the Y value of the function
            float yVal = GetSmoothLog(xVal);
            float normalizeYVal = yVal / yCutOff;
            float fracJourney = normalizeYVal / 1;

            return Mathf.Lerp(lastTarget, target, fracJourney);
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
