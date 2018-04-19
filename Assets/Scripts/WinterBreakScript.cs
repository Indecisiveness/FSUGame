using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinterBreakScript : MonoBehaviour {

    int counter, counter2;
    string clickText;
    GameObject winterGame;
    GameObject clickMe;
    GameObject gamePanel;
    GameObject winButton;
    bool startGame = false;


    public int speed;
    public int xLimit;
    public int yLimit;

	// Use this for initialization
	void Start () {
        counter = 0;
        counter2 = 0;

        clickMe = GameObject.Find("ClickMe");
        clickText = GameObject.Find("ClickText").GetComponent<TextMesh>().text;
        winterGame = GameObject.Find("WinterGame");
        gamePanel = GameObject.Find("GamePanel");
        winButton = GameObject.Find("WinButton");

        gamePanel.SetActive(false);
        winterGame.SetActive(false);
        winButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        counter++;
        counter2++;

        if (counter == 60)
            counter = 0;

        if (counter < 31)
            GameObject.Find("ClickText").GetComponent<TextMesh>().text = "";
        else
            GameObject.Find("ClickText").GetComponent<TextMesh>().text = clickText;

        if(startGame)
        {
            if (counter2 % (60 - speed) == 0)
                moveClickMe();
        }
    }

    public void openGame()
    {
        winterGame.SetActive(true);
        gamePanel.SetActive(true);
    }

    public void beginGame()
    {
        startGame = true;
        GameObject.Find("TimeLeftText").GetComponent<CountdownTimerScript>().start = true;
    }


    void moveClickMe()
    {
        Vector3 position = clickMe.transform.position;
        position.x = Random.Range(-xLimit, xLimit);
        position.y = Random.Range(-yLimit, yLimit);
        clickMe.transform.position = position;
    }

    public void isClicked()
    {
        winButton.SetActive(true);
        startGame = false;
        GameObject.Find("TimeLeftText").GetComponent<CountdownTimerScript>().start = false;
    }
}
