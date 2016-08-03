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
		public static int numEnemiesOnMap;
		public static int maxEnemiesPossibleOnMap;

		public static bool needMoreEnemies() {
			return numEnemiesOnMap < maxEnemiesPossibleOnMap;
		}

        void Awake()
        {
			numEnemiesOnMap = 0;
			maxEnemiesPossibleOnMap = 30;
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

		public void RageQuit() {
			/*
			 * Quit is ignored in the editor. IMPORTANT: In most cases termination of application under iOS 
			 * should be left at the user discretion. Consult Apple Technical Page qa1561 for further details.
			 */
			print ("Quit if running as application.");
			Application.Quit ();
		}
    }
}
