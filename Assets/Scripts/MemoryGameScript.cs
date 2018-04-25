using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class provides a memory game consisting of twelve memory cards - six pairs of six colors. If the player gets all six pairs before time runs out, a "You Win" 
 * message is displayed.  If not, a "You Lose" message is displayed. 
 */ 
public class MemoryGameScript : MonoBehaviour {

    public GameObject[] memoryCards;
    public GameObject[] hideCards;
    public int card1, card2, pairs;
    Color[] colors;
    MemoryTimerScript memoryTimerScript;
    GameObject gameEndPanel;


	// Use this for initialization
	void Start () {

        pairs = 0;

        memoryTimerScript = GameObject.Find("TimeLeftText2").GetComponent<MemoryTimerScript>();
        gameEndPanel = GameObject.Find("GameEndPanel");

        gameEndPanel.SetActive(false);

        memoryCards = new GameObject[12];
        hideCards = new GameObject[12];
        colors = new Color[] { Color.blue, Color.blue, Color.green, Color.green, Color.red, Color.red,
            Color.yellow, Color.yellow, Color.magenta, Color.magenta, Color.gray, Color.gray, };

        card1 = -1;

		for(int i = 0; i < memoryCards.Length; i++)
        {
            memoryCards[i] = this.transform.GetChild(i).gameObject;
            memoryCards[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            hideCards[i] = this.transform.GetChild(i + 12).gameObject;
            hideCards[i].GetComponent<SpriteRenderer>().color = Color.black; 
            hideCards[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    //This method starts the game by setting random colors to the cards and hides them 5 seconds later
    public void startGame()
    {
        setRandomColors();
        StartCoroutine("hideMemoryCards");
    }

    //This method sets random colors to the 12 cards.  There are 6 colors, and the set of 12 cards will contain two of each color
    void setRandomColors()
    {
        float top = 11;
        int colorIdx;

        for( int i = 0; i < memoryCards.Length; i++)
        {
            colorIdx = (int) Random.Range(0, top);
            memoryCards[i].GetComponent<SpriteRenderer>().color = colors[colorIdx];
            colors[colorIdx] = colors[(int) top];
            top--;
        }

        GameObject.Find("ShowCards").SetActive(false);
    }

    //This method hides the memoryCards after they have been displayed for 5 seconds.  Once the cards are hidden the timer starts
    IEnumerator hideMemoryCards()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < memoryCards.Length; i++)
        {
            hideCards[i].SetActive(true);
        }

        //Start timer
        memoryTimerScript.start = true;
    }

    //This method checks and stores information about the selected memory card
    public void checkMemoryCard(int idx)
    {
        //If this if the first card drawn, card 1 is set to its index
        if (card1 == -1)
        {
            card1 = idx;
        }
        //If this is the second card drawn, card 2 is set to its index
        else
        {
            card2 = idx;

            //If the colors of the cards don't match, hide the two selected cards after 200 milliseconds
            if (!memoryCards[card1].GetComponent<SpriteRenderer>().color.Equals(memoryCards[card2].GetComponent<SpriteRenderer>().color))
            {
                StartCoroutine("hideSelectedCards");
            }
            //If the colors of the cards match, reset card1 to -1 and increment pairs
            else
            {
                card1 = -1;
                pairs++;

                //If this is the sixth pair, end the game and display "You Win"
                if (pairs == 6)
                    endGame("You Win!!!");
            }
        }
    }

    //This method hides the two memory cards displayed after 200 milliseconds
    IEnumerator hideSelectedCards()
    {
        yield return new WaitForSeconds(.2f);
        hideCards[card1].SetActive(true);
        hideCards[card2].SetActive(true);

        card1 = -1;
    }

    //This method ends the game and displays if the player won or lost
    public void endGame(string str)
    {
        memoryTimerScript.start = false;
        gameEndPanel.SetActive(true);
        gameEndPanel.GetComponentInChildren<Text>().text = str;
    }
}
