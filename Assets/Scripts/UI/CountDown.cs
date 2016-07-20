using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;

public class CountDown : MonoBehaviour {

    private bool doCountDown = false;
    private float startingTime;
    private float countDownFrom = 3;
    private int intCountDown;
    public Text countDownText;
    public GameObject playerUI;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	    if(doCountDown)
        {
            intCountDown = (int)(countDownFrom - (Time.time - startingTime));
            countDownText.text = intCountDown.ToString();

            if(intCountDown <= 0)
            {
                doCountDown = false;
                gameObject.SetActive(false);
                playerUI.gameObject.SetActive(true);
            }
        }
	}

    public void StartCountDown()
    {
        ManageGameState.SetPause(false);
        startingTime = Time.time;
        doCountDown = true;
    }
}
