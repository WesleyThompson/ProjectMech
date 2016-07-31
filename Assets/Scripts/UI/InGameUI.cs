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
		public Button resumePlayerBtn;
        public Button restartPlayerBtn;
        public Button returnToMainMenuPlayerBtn;
        public Button optionsPlayerBtn;
        public Button exitPlayerBtn;

        public GameObject panel;


        private Health playerHealthScript;
        private Energy playerEnergyScript;

		public GameObject optionsImage;
		public GameObject cancelButton;

        // Use this for initialization
        void Start()
		{
			playerHealthScript = GameObject.Find (GlobalVariables.PlayerName).GetComponent<Health> ();
			playerEnergyScript = GameObject.Find (GlobalVariables.PlayerName).GetComponent<Energy> ();
		}
        
        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                ManageGameState.TogglePause();
            }
        }
	
		private bool displayOptions = true;
		public void showOptions() {
			optionsImage.SetActive (displayOptions);
			cancelButton.SetActive (displayOptions);
			displayOptions = !displayOptions;
		}
    }
}
