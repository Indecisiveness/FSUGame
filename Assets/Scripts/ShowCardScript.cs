using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is to hide the "hideCard" in the memory game, in effect displaying the memory card.
 */
public class ShowCardScript : MonoBehaviour {

    MemoryGameScript memoryGameScript;
	// Use this for initialization
	void Start () {
        memoryGameScript = GetComponentInParent<MemoryGameScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showCard()
    {
        //Hide this hideCArd and display the color card
        this.gameObject.SetActive(false);

        //Call method with its index as an argument to check if the memory card is the first or second card selected. If it is the second, check to see if they match
        memoryGameScript.checkMemoryCard(int.Parse(this.gameObject.name.ToString().Substring(4, 2).ToString()));
    }
}
