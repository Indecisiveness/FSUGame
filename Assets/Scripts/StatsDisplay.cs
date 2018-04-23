using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Original Author: Mark Roberts
/// Created: 17 March 2018
/// Function: This class creates the objects required for the status display
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
	public Text socialTime;
	public Text studyTime;
	public Text workTime;
	public Text GPA;
	public Text classStanding;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void fillFields(){
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
		socialTime.text = "" + playerScript.timeSocial.ToString ("0.##\\%");
		studyTime.text = "" + playerScript.timeStudy.ToString ("0.##\\%");
		workTime.text = "" + playerScript.timeWork.ToString ("0.##\\%");
		GPA.text = "" + playerScript.myTrans.gpa;
		classStanding.text = "" + playerScript.myTrans.classStanding;

	}
}
