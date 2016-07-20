using UnityEngine;
using System.Collections;
using Common;

public class StartingUI : MonoBehaviour {
    public GameObject player;
    public GameObject countdownUI;
	// Use this for initialization

    void Awake()
    {
        GameObject p = Instantiate(player, new Vector3(0, 3.3f, 0), Quaternion.identity) as GameObject;
        p.transform.eulerAngles = new Vector3(0, 90, 0);
        p.name = GlobalVariables.PlayerName;
    }

	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.anyKeyDown)
        {
            //Start Countdown
            countdownUI.SetActive(true);
            countdownUI.GetComponent<CountDown>().StartCountDown();
            gameObject.SetActive(false);
        }
	}
}
