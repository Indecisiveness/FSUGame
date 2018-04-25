using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class provides a countdown timer for the winter break game.  The timer counts down in increments of tenths of a second and is displayed in the winter game board. 
 * If time has run out, a "You Lose" message is displayed.
 */
public class CountdownTimerScript : MonoBehaviour {

    int counter;
    int tenthsOfSecond;
    public int seconds;
    public bool start;
    
    Text timeLeft;
    WinterBreakScript winterBreakScript;

	// Use this for initialization
	void Start () {
        winterBreakScript = GameObject.Find("WinterGO").GetComponent<WinterBreakScript>();
        counter = 0;
        tenthsOfSecond = 0;
        start = false;

        //Initializing timeLEft to the Text object displaying time left
        timeLeft = GameObject.Find("TimeLeftText").GetComponent<Text>();
        timeLeft.text = seconds.ToString() + "." + tenthsOfSecond.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if(start)
        {
            //counter increments 60 times per second
            counter++;

            //if 0.1 seconds have elapsed
            if (counter % 6 == 0)
            {
                if (tenthsOfSecond == 0)
                {
                    tenthsOfSecond = 10;
                    seconds--;
                }

                tenthsOfSecond--;
            }

            //Remaining time is displayed in white text until there are five seconds remaining.  Then the text is red
            if (seconds < 5) { timeLeft.color = Color.red; }
            else { timeLeft.color = Color.white; }

            //Display remaining time un UI
            timeLeft.text = seconds.ToString() + "." + tenthsOfSecond.ToString();


            //If time runs out, end game and display you lose
            if (seconds == 0 && tenthsOfSecond == 0)
            {
                winterBreakScript.clickMe.SetActive(false);
                winterBreakScript.winButton.SetActive(true);
                winterBreakScript.winButton.GetComponentInChildren<Text>().text = "YOU LOSE!!!";
                winterBreakScript.startGame = false;
                start = false;
            }
        }
	}
}
