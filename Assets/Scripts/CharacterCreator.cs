using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


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

		//retrieve index of option selected, 0-2, and give the associated stat list

		MajorList AllMajors =Resources.Load<MajorList>("AllMajors");

		int ThisMajor = majorChoice.value;

		Major MyMajor = AllMajors.MyMajors [ThisMajor];


		Major MyGenEds = AllMajors.MyMajors.Find(x => x.MajorName == "GenEd");

		mainChar.myTrans = TranscriptBuilder.Create (MyMajor, MyGenEds);
	

		mainChar.StayAlive();

	}




}


