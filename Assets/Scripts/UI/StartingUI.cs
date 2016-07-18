using UnityEngine;
using System.Collections;
using Common;

public class StartingUI : MonoBehaviour {
    public GameObject player;
    public GameObject playerUI;
	// Use this for initialization
	void Start ()
    {
        GameObject p = Instantiate(player, new Vector3(0, 3.3f, 0), Quaternion.identity) as GameObject;
        p.transform.eulerAngles = new Vector3(0, 90, 0);
        p.name = GlobalVariables.PlayerName;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.anyKeyDown)
        {
            //Start Countdown
            playerUI.gameObject.SetActive(true);
            ManageGameState.SetPause(false);
            gameObject.SetActive(false);
        }
	}
}
