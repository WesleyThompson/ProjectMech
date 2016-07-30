using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Player;

namespace Common
{
    public class InGameUI : MonoBehaviour
    {
        [Space(10)]
        [Header("Player Pause UI")]
        public GameObject inGameMenuPanel;
        public Button xMarkBtn;
        public Button resumePlayerBtn;
        public Button restartPlayerBtn;
        public Button returnToMainMenuPlayerBtn;
        public Button optionsPlayerBtn;
        public Button exitPlayerBtn;

        public GameObject panel;


        private Health playerHealthScript;
        private Energy playerEnergyScript;

        // Use this for initialization
        void Start()
        {

            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();

            //Player Pause UI

            xMarkBtn.onClick.AddListener(() =>
            {
                ManageGameState.TogglePause();
            });

            resumePlayerBtn.onClick.AddListener(() =>
            {
                ManageGameState.TogglePause();
            });

            restartPlayerBtn.onClick.AddListener(() =>
            {
                print("TODO: Restart Game");
            });

            returnToMainMenuPlayerBtn.onClick.AddListener(() =>
            {
                print("TODO: Return to Main Menu");
            });

            optionsPlayerBtn.onClick.AddListener(() =>
            {
                print("TODO: Open Options");
            });

            exitPlayerBtn.onClick.AddListener(() =>
            {
                ManageGameState.TogglePause();
            });
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                ManageGameState.TogglePause();
            }
        }
    }
}
