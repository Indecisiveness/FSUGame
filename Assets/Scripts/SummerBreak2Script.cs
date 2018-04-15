using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerBreak2Script : MonoBehaviour {

    int counter, counter2;
    string clickText;
    GameObject summerGame2;
    GameObject clickMe;
    GameObject gamePanel;
    GameObject winButton;
    bool startGame = false;


    public int speed;
    public int xLimit;
    public int yLimit;

    // Use this for initialization
    void Start()
    {
        counter = 0;
        counter2 = 0;

        clickMe = GameObject.Find("ClickMe");
        clickText = GameObject.Find("ClickText").GetComponent<TextMesh>().text;
        summerGame2 = GameObject.Find("SummerGame2");
        gamePanel = GameObject.Find("GamePanel");
        winButton = GameObject.Find("WinButton");

        gamePanel.SetActive(false);
        summerGame2.SetActive(false);
        winButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        counter2++;

        if (counter == 60)
            counter = 0;

        if (counter < 31)
            GameObject.Find("ClickText").GetComponent<TextMesh>().text = "";
        else
            GameObject.Find("ClickText").GetComponent<TextMesh>().text = clickText;

        if (startGame)
        {
            if (counter2 % (60 - speed) == 0)
                moveClickMe();
        }
    }

    public void openGame()
    {
        summerGame2.SetActive(true);
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