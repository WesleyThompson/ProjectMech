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

        private float uiSpeed = 10;
        private float baseVal = 2f;
        private float yCutOff = 10f;

        protected float healthUI;
        public float energyUI;
        public float energyNormal;
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
            energyNormal = energyUI;
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
                float totalTime = 1f;
                float xCutoff = (Mathf.Pow(baseVal, yCutOff) - 1 / uiSpeed)/ uiSpeed;
                float elapsedTime = Time.time - playerEnergyScript.GetStartTimeEnergy();
                float xVal = xCutoff * elapsedTime / totalTime;
                float yVal = Mathf.Log((xVal + (1 / uiSpeed)) * uiSpeed, baseVal);
                float normalizeYVal = yVal / yCutOff;
                float fracJourney = normalizeYVal / totalTime;//Mathf.Abs(playerEnergyScript.GetLastTargetEnergy()- playerEnergyScript.GetTargetEnergy());
                energyUI = Mathf.Lerp(playerEnergyScript.GetLastTargetEnergy(), playerEnergyScript.GetTargetEnergy(), fracJourney);
                UpdateEnergyBarImg();
                //print(Time.time - playerEnergyScript.GetStartTimeEnergy());
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
