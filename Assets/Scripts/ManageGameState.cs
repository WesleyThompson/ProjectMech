using UnityEngine;
using System.Collections;

namespace Common
{
    public class ManageGameState : GameBehavior
    {
        public static bool isPaused;
        public GameObject inGameMenu;
        private static InGameUI inGameUIScript;

        void Awake()
        {
            inGameUIScript = GameObject.Find(GlobalVariables.World).GetComponent<InGameUI>();
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
            inGameUIScript.inGameMenuPanel.SetActive(isPaused);
        }
    }
}
