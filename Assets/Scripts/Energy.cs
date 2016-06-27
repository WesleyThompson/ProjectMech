using UnityEngine;
using System.Collections;

namespace Player
{
    public class Energy : MonoBehaviour {

        private float maxPlayerEnergy = 100;
        private float playerEnergy;

        private float lastTargetEnergy;
        private float targetEnergy;
        private float startTimeEnergy;

        private PlayerUI playerUIScript;

        void Awake()
        {
            playerUIScript = GameObject.Find(GlobalVariables.PlayerUI).GetComponent<PlayerUI>();
        }

        void Start()
        {
            SetPlayerEnergy(maxPlayerEnergy);
            lastTargetEnergy = targetEnergy = playerEnergy;
        }

        public void UsePlayerEnergy(float energy)
        {
            lastTargetEnergy = playerUIScript.energyUI;
            startTimeEnergy = Time.time;
            SetPlayerEnergy(playerEnergy - energy);
            targetEnergy = playerEnergy;

            if (playerEnergy <= 0)
            {
                playerEnergy = 0;
            }
        }

        private void SetPlayerEnergy(float e)
        {
            playerEnergy = e;
            playerUIScript.UpdateEnergyBarImg();
        }

        public float GetEnergy()
        {
            return playerEnergy;
        }

        public float GetMaxEnergy()
        {
            return maxPlayerEnergy;
        }

        public float GetLastTargetEnergy()
        {
            return lastTargetEnergy;
        }

        public float GetTargetEnergy()
        {
            return targetEnergy;
        }

        public float GetStartTimeEnergy()
        {
            return startTimeEnergy;
        }
    }
}
