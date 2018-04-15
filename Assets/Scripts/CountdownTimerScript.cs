using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerScript : MonoBehaviour {

    int counter;
    public int seconds;
    public bool start;
    int tenthsOfSecond;
    Text timeLeft;

	// Use this for initialization
	void Start () {
        counter = 0;
        tenthsOfSecond = 0;
        start = false;
        timeLeft = GameObject.Find("TimeLeftText").GetComponent<Text>();

        timeLeft.text = seconds.ToString() + "." + tenthsOfSecond.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if(start)
        {
            counter++;

            if (counter % 6 == 0)
            {
                if (tenthsOfSecond == 0)
                {
                    tenthsOfSecond = 10;
                    seconds--;
                }

                tenthsOfSecond--;
            }

            if (seconds < 11)
                timeLeft.color = Color.red;
            else
                timeLeft.color = Color.black;

            timeLeft.text = seconds.ToString() + "." + tenthsOfSecond.ToString();

            if (seconds == 0 && tenthsOfSecond == 0)
            {
                //gameOver();
            }
        }
        
            

	}
}
