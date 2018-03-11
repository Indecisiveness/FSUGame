using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckScript : MonoBehaviour {

	public List<int> ToDraw;              //List of card values

	public Button card;                   //base card

	// Use this for initialization
	void Start () {
		ToDraw = new List<int>{1,2,3,4,5,6,7,8,9,10}; //List of values		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DrawCard (){
		int value = (int)(Random.value * ToDraw.Count);  //random number between 0 and length-1
		int cardNo = ToDraw [value];                     //get contents at index
		ToDraw.RemoveAt (value);                         //delete contents at index

		Button DrawnCard = Instantiate<Button> (card,card.transform);  //create new card object
		Text myText = DrawnCard.GetComponentInChildren<Text>();     //retrieve object's text
		myText.text = "" + cardNo;                                  //set text equal to pulled value as string
		DrawnCard.interactable = true;                              //turn button on to allow for self-deletion
		Button myButton = gameObject.GetComponent<Button>();        //get the Button this is attached to
		myButton.interactable = false;                              //turns off this button until card turns it on again
	}

}
