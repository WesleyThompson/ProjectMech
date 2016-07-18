using UnityEngine;
using System.Collections;

namespace Common
{
    public class ManageGameState : GameBehavior
    {
        public static bool isPaused = true;
        public GameObject inGameMenu;
        private static InGameUI inGameUIScript;
        private static GameObject player;
        
        void Start()
        {
            inGameUIScript = GameObject.Find(GlobalVariables.World).GetComponent<InGameUI>();
            player = GameObject.Find(GlobalVariables.PlayerName);
            SetPause(isPaused);
        }

        public static void SetPause(bool setActive)
        {
            isPaused = setActive;
            if (isPaused)
            {
                Time.timeScale = 0;
                print(player.transform.FindChild("tankTurret").FindChild("GunJoint").GetComponent<RotateTankGuns>());
                player.transform.FindChild("tankTurret").FindChild("GunJoint").GetComponent<RotateTankGuns>().enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                player.transform.FindChild("tankTurret").FindChild("GunJoint").GetComponent<RotateTankGuns>().enabled = true;
            }
        }

        public static void SetInGamePanel(bool setActive)
        {
            SetPause(setActive);
            inGameUIScript.inGameMenuPanel.SetActive(setActive);
        }
    }
}
