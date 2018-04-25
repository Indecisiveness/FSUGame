using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class provides a countdown timer for the summer break game.  The timer counts down in increments of tenths of a second and is displayed in the summer game board. 
 * If time has run out, a "You Lose" message is displayed.
 */
public class MemoryTimerScript : MonoBehaviour
{
    int counter;
    int tenthsOfSecond;

    public int seconds;     //Time alotted for game
    public bool start;

    Text timeLeft;

    MemoryGameScript memoryGameScript;

    // Use this for initialization
    void Start()
    {
        memoryGameScript = GameObject.Find("ColorSquares").GetComponent<MemoryGameScript>();
        counter = 0;
        tenthsOfSecond = 0;
        start = false;
        timeLeft = GameObject.Find("TimeLeftText2").GetComponent<Text>();

        timeLeft.text = "Time Left:        " + seconds.ToString() + "." + tenthsOfSecond.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
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
            if (seconds < 5)
                timeLeft.color = Color.red;
            else
                timeLeft.color = Color.black;

            //Display remaining time on UI
            timeLeft.text = "Time Left:        " + seconds.ToString() + "." + tenthsOfSecond.ToString();

            //If time runs out, end game and display you lose
            if (seconds == 0 && tenthsOfSecond == 0)
            {
                start = false;
                memoryGameScript.endGame("You Lose!!!");
            }
        }
    }
}
