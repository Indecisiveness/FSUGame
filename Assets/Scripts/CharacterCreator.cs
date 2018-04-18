//Function for naming and making a transcript for a character at creation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterCreator : MonoBehaviour {


	public PlayerScript mainChar;   //character to be given stats

	public InputField namePicker;  //Text box for user-inputted string

	public Dropdown majorChoice;







	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void characterCreation(){

		Debug.Log ("Character Created");

		mainChar.charName = namePicker.text;  //set character object name to the text entered

        
		MajorList AllMajors = Resources.Load<MajorList>("myMajorList"); //Get a list of majors

		int ThisMajor = majorChoice.value; //find the major that was picked

		Major MyMajor = AllMajors.MyMajors [ThisMajor]; //retrieve the major



		mainChar.myTrans = TranscriptBuilder.Create (MyMajor); //create a transcript for that major
        
	

	}




}


