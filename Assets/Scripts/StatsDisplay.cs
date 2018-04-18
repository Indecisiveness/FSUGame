﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Original Author: Mark Roberts
/// Created: 17 March 2018
/// Fundtion: This class creates the objects required for the status display
/// </summary>
public class StatsDisplay : MonoBehaviour {

	public GameObject myPlayer;
	public PlayerScript playerScript;

	public Text playerName;
	public Text jobSkills;
	public Text studyHabits;
	public Text socialSkills;
	public Text physicalHealth;
	public Text sanity;
	public Text motivation;
	public Text finances;
	public Text GPA;
	public Text classStanding;
	public Text socialTime;
	public Text studyTime;
	public Text workTime;
	public Text currentYear;

	// Use this for initialization
	void Start () {

		myPlayer = GameObject.FindGameObjectWithTag ("Player");
		playerScript = myPlayer.GetComponent<PlayerScript> ();

		playerName.text = playerScript.charName;
		jobSkills.text = "" + playerScript.charStats [0];
		studyHabits.text = "" + playerScript.charStats [1];
		socialSkills.text = "" + playerScript.charStats [2];
		physicalHealth.text = "" + playerScript.charStats [3];
		sanity.text = "" + playerScript.charStats [4];
		motivation.text = "" + playerScript.charStats [5];
		finances.text = "" + playerScript.charStats [6];
		GPA.text = "" + playerScript.myTrans.gpa;
		socialTime.text = "" + playerScript.timeSocial.ToString ("0.##\\%");
		studyTime.text = "" + playerScript.timeStudy.ToString ("0.##\\%");
		workTime.text = "" + playerScript.timeWork.ToString ("0.##\\%");

	}
	
	// Update is called once per frame
	void Update () {
	}
}
