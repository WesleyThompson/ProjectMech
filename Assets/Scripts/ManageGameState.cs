using UnityEngine;
using System.Collections;

namespace Common
{
    public class ManageGameState : GameBehavior
    {
        public static bool isPaused;
        public GameObject inGameMenu;
        private static ManageUIClicks manageUIClicksScript;

        void Awake()
        {
            manageUIClicksScript = GameObject.Find(GlobalVariables.World).GetComponent<ManageUIClicks>();
            isPaused = inGameMenu.activeSelf;
        }

        public static void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            manageUIClicksScript.inGameMenuPanel.SetActive(isPaused);
        }
    }
}
