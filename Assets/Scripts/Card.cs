using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	List<int> Modifiers;

	public Button FromDeck;  //deck that card was drawn from


	// Use this for initialization
	void Start () {
		Modifiers = new List<int> {1, 0, 0, 0, 0, 0};
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SelfDestruct(){
		FromDeck.interactable = true;      //re-enables button
		Destroy (gameObject);              //get rid of game object script is attached to
	}

	public void GenerateEffect(){                  //Generate Card effect
		PlayerScript MyChar = FindObjectOfType<PlayerScript>();

		List<int> CurrentStats = MyChar.charStats;    //Retrieves current player stats
		for (int i = 0; i < CurrentStats.Count; i++) {    //Retrieves each int in sequence
			CurrentStats [i] = Modifiers [i] + CurrentStats [i];  //modifies ints by modifiers
		}

		MyChar.charStats = CurrentStats;      //sets character stats to new values

		SelfDestruct ();
	}

}
