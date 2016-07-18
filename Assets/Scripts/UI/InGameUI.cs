using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Player;

namespace Common
{
    public class InGameUI : MonoBehaviour
    {
        [Header("Player")]
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

        // Use this for initialization
        void Start()
        {

            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();

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
                ManageGameState.SetInGamePanel(false);
            });

            resumePlayerBtn.onClick.AddListener(() =>
            {
                ManageGameState.SetInGamePanel(false);
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
                ManageGameState.SetInGamePanel(false);
            });
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                ManageGameState.SetInGamePanel(!ManageGameState.isPaused);
            }
        }
    }
}
