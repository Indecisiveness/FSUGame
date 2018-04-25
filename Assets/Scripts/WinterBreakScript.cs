using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class provides a game where the player has to click on Santa before time runs out.  If they succeed before time 
 * runs out, a "You Win" message is displayed.  If not, a "You Lose" message is displayed.
 */
public class WinterBreakScript : MonoBehaviour {

    int counter, counter2;
    string clickText;
    GameObject winterGame;
    public GameObject clickMe;
    GameObject gamePanel;
    public GameObject winButton;
    public bool startGame = false;

    public int speed;
    public int xLimit;
    public int yLimit;

	// Use this for initialization
	void Start () {
        counter = 0;
        counter2 = 0;
        
        winterGame = GameObject.Find("WinterGame");
        gamePanel = GameObject.Find("GamePanel");
        winButton = GameObject.Find("WinButton");
        clickMe = GameObject.Find("ClickMe");
        clickText = GameObject.Find("ClickText").GetComponent<TextMesh>().text;

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

        //Toggletext on and off in intervals of 500 milliseconds
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

    //This method displays the game and its associated UIs
    public void openGame()
    {
        winterGame.SetActive(true);
        gamePanel.SetActive(true);
    }

    //This method shrinks the santa picture, enables him to be clicked, and starts the timer and game
    public void beginGame()
    {
        //shrink santa picture
        Vector3 scale = clickMe.transform.localScale;
        scale.x = .13f;
        scale.y = .15f;
        clickMe.transform.localScale = scale;

        //enables santa to be clicked
        clickMe.AddComponent<BoxCollider2D>();
        startGame = true;

        //starts timer
        GameObject.Find("TimeLeftText").GetComponent<CountdownTimerScript>().start = true;
    }

    //This method moves the santa picture to a random point in the game area
    void moveClickMe()
    {
        Vector3 position = clickMe.transform.position;
        position.x = Random.Range(-xLimit, xLimit);
        position.y = Random.Range(-yLimit, yLimit);
        clickMe.transform.position = position;
    }

    //This method is for if the user clicks santa before time end.  If so, the game is ended and a button will appear saying "YOU WIN!!!".
    public void isClicked()
    {
        clickMe.SetActive(false);
        winButton.SetActive(true);
        winButton.GetComponentInChildren<Text>().text = "YOU WIN!!!";
        startGame = false;
        GameObject.Find("TimeLeftText").GetComponent<CountdownTimerScript>().start = false;
    }
}
