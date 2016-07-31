using UnityEngine;
using UnityEngine.UI;
using Player;
using UnityEngine.SceneManagement;

namespace Common
{
    public class ManageUIClicks : GameBehavior
    {
        [Header("Main Menu")]
        public Button newGameBtn;
        public Button continueGameBtn;
        public Button optionsBtn;

		public GameObject optionsImage;

        void Start()
        {
            //DontDestroyOnLoad(transform.gameObject);
            SetIsUIFocusedEvent();

            newGameBtn.onClick.AddListener(() =>
            {
                //Function call to start game
				SceneManager.UnloadScene("Splash_Screen");
				SceneManager.LoadScene("Main_Scene");
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
				showOptions();
            });
        }

        private void SetIsUIFocusedEvent()
        {
            //This is an example of how to call Focusable
            //Focusable(panel);
        }

		private bool displayOptions = false;
		public void showOptions() {
			optionsImage.SetActive (displayOptions = !displayOptions);
		}
    }
}
