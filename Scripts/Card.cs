using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	List<int> Modifiers;
    public int studyMod;
    public int workMod;
    public int socialMod;
    public int healthMod;
    public int sanityMod;
    public int motivationMod;
    public int financeMod;
    public double gpaMod;

	//public Button FromDeck;  //deck that card was drawn from
    GameObject player;
    PlayerScript playerScript;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        Modifiers = new List<int> {studyMod, workMod, socialMod,healthMod, sanityMod, motivationMod, financeMod}; // study,work,social,health,sanity,motivation,finance
        
	}
	
	                                                                
	void Update () {
		
	}


	public void SelfDestruct(){
		//FromDeck.interactable = true;                               //re-enables button
		Destroy (gameObject);                                       //get rid of game object script is attached to
	}

	public void GenerateEffect(){                                  //Generate Card effect
		

		List<int> CurrentStats = playerScript.charStats;            //Retrieves current player stats
		for (int i = 0; i < CurrentStats.Count; i++) {              //Retrieves each int in sequence
			CurrentStats [i] = Modifiers [i] + CurrentStats [i];    //modifies ints by modifiers
		}

        playerScript.charStats = CurrentStats;                      //sets character stats to new values
        playerScript.gpa += gpaMod;                                 // add gpa double modifier
		SelfDestruct ();
	}

}
