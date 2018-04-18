﻿//class for the semester end screen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SemesterEnd : MonoBehaviour {

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
    public Text currentYear;
    public Text coursesDone;

    // Use this for initialization
    void Start () {

        myPlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = myPlayer.GetComponent<PlayerScript>();

		playerScript.myTrans.CompleteSemester ();

       // playerName.text = playerScript.charName;
        jobSkills.text = "" + playerScript.charStats[0];
        studyHabits.text = "" + playerScript.charStats[1];
        socialSkills.text = "" + playerScript.charStats[2];
        physicalHealth.text = "" + playerScript.charStats[3];
        sanity.text = "" + playerScript.charStats[4];
        motivation.text = "" + playerScript.charStats[5];
        finances.text = "" + playerScript.charStats[6];
        GPA.text = "" + playerScript.myTrans.gpa;
        coursesDone.text = "" + playerScript.myTrans.coursesFinished;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
