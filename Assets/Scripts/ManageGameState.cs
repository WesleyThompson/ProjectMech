using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Common
{
    public class ManageGameState : GameBehavior
    {
        public static bool isPaused;
        public GameObject inGameMenu;
        private static InGameUI inGameUIScript;
		private static AudioSource backgroundMusic;

        void Awake()
        {
            inGameUIScript = GameObject.Find(GlobalVariables.World).GetComponent<InGameUI>();
            isPaused = inGameMenu.activeSelf;

			backgroundMusic = GetComponent<AudioSource> ();
        }
		public void PausePlay() {
			TogglePause ();
		}

		public static IEnumerator GameOver()
		{
			yield return new WaitForSeconds (2);
			SceneManager.LoadScene ("Main_Scene");
		}

        public static void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
				backgroundMusic.Pause ();
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
				backgroundMusic.Play ();
				Cursor.lockState = CursorLockMode.Locked;
            }
            inGameUIScript.inGameMenuPanel.SetActive(isPaused);
        }

		public void Restart() {
			TogglePause ();
			SceneManager.LoadScene("Main_Scene");
		}

		public void MainMenu() {
			TogglePause ();
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene ("Splash_Screen");
		}
    }
}
