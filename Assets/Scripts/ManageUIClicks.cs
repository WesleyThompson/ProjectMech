using UnityEngine;
using UnityEngine.UI;
using Player;

namespace Common
{
    public class ManageUIClicks : GameBehavior
    {

        public Button newGameBtn;
        public Button continueGameBtn;
        public Button optionsBtn;
        public Button takeDamageBtn;
        public Button useEnergyBtn;

        public GameObject panel;

        private Health playerHealthScript;
        private Energy playerEnergyScript;

        void Start()
        {
            playerHealthScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Health>();
            playerEnergyScript = GameObject.Find(GlobalVariables.PlayerName).GetComponent<Energy>();
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
                playerEnergyScript.UsePlayerEnergy(40);
            });
        }

        private void SetIsUIFocusedEvent()
        {
            //This is an example of how to call Focusable
            //Focusable(panel);
        }
    }
}
