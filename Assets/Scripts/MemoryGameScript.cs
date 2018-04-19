using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MemoryGameScript : MonoBehaviour {

    public GameObject[] memoryCards;
    public GameObject[] hideCards;
    public int card1, card2;
    int idx;
    Color[] colors;
    public int counter;


	// Use this for initialization
	void Start () {

        counter = 0;

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
            hideCards[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        counter++;
	}

    public void startGame()
    {
        setRandomColors();
    }

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

        //GameObject.Find("ShowCards").SetActive(false);
    }

    public void hideMemoryCards()
    {
        for(int i = 0; i < memoryCards.Length; i++)
        {
            hideCards[i].SetActive(true);
        }
    }

    public void wait(int idx)
    {
        this.idx = idx;

        

        showMemoryCard();

    }

    public void showMemoryCard()
    {
        //hideCards[idx].SetActive(false);

        if (card1 == -1)
        {
            card1 = idx;
        }
        else
        {
            card2 = idx;

            if (!memoryCards[card1].GetComponent<SpriteRenderer>().color.Equals(memoryCards[card2].GetComponent<SpriteRenderer>().color))
            {
                StartCoroutine("hideSelectedCards");
                
                //hideCards[card1].SetActive(true);
                //hideCards[card2].SetActive(true);
            }
            else
            {
                card1 = -1;
            }

            //card1 = -1;
        }
    }
    IEnumerator hideSelectedCards()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(card1.ToString() + card2.ToString());
        hideCards[card1].SetActive(true);
        hideCards[card2].SetActive(true);

        card1 = -1;
    }

    public void endGame()
    {

    }
}
