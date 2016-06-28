using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Common
{
    public class ManageUIClicks : GameBehavior
    {
        [Header("Main Menu")]
        public Button newGameBtn;
        public Button continueGameBtn;
        public Button optionsBtn;
        public Button takeDamageBtn;
        public Button useEnergyBtn;

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
        private bool isPaused;

        void Start()
        {
            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();
            isPaused = false;
            SetIsUIFocusedEvent();

            newGameBtn.onClick.AddListener(() =>
            {
                //Function call to start game
                print("TODO: Add functionality to start game.");
            });

            continueGameBtn.onClick.AddListener(() =>
            {
                //Function call to continue
                print("TODO: Add functionality to continue.");
            });

            optionsBtn.onClick.AddListener(() =>
            {
                //Function call to options
                print("TODO: Add functionality to options.");
            });

            takeDamageBtn.onClick.AddListener(() =>
            {
                playerHealthScript.TakeDamage(10);
            });

            useEnergyBtn.onClick.AddListener(() =>
            {
                playerEnergyScript.UsePlayerEnergy(30);
            });

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
            if(Input.GetButtonDown("Cancel"))
            {
                ManageGameState.TogglePause();
            }
        }

        

        private void SetIsUIFocusedEvent()
        {
            //This is an example of how to call Focusable
            //Focusable(panel);
        }
    }
}
