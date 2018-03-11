using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour {


	public PlayerScript mainChar;   //character to be given stats

	public InputField namePicker;  //Text box for user-inputted string

	public Dropdown classChoice;  //dropdown menu for class choice


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void characterCreation(){

		Debug.Log ("Character Created");

		mainChar.charName = namePicker.text;  //set character object name to the text entered

		int classChosen = classChoice.value; //retrieve index of option selected, 0-2, and give the associated stat list

		if (classChosen == 0){
			mainChar.charStats = new List<int>{ 3, 0, 0, 0, 0, 3};
		}
		if (classChosen == 1){
			mainChar.charStats = new List<int>{ 0, 3, 0, 0, 3, 0};
		}
		if (classChosen == 2){
			mainChar.charStats = new List<int>{0, 0, 3, 0, 3, 0, 0};
		}

		mainChar.StayAlive();

	}




}


