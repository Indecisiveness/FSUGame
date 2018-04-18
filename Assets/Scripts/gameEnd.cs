using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameEnd : MonoBehaviour {

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

    public float finalScore;

    public Text Score;

    // Use this for initialization
    void Start () {

        myPlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = myPlayer.GetComponent<PlayerScript>();

        finalScore = (((playerScript.myTrans.gpa / 4) * 50) + (((playerScript.charStats[0] + playerScript.charStats[1] + playerScript.charStats[2] + playerScript.charStats[3] + playerScript.charStats[4] + playerScript.charStats[5] + playerScript.charStats[6]) / 70) * 50));

        playerName.text = playerScript.charName;
        jobSkills.text = "" + playerScript.charStats[0];
        studyHabits.text = "" + playerScript.charStats[1];
        socialSkills.text = "" + playerScript.charStats[2];
        physicalHealth.text = "" + playerScript.charStats[3];
        sanity.text = "" + playerScript.charStats[4];
        motivation.text = "" + playerScript.charStats[5];
        finances.text = "" + playerScript.charStats[6];
        GPA.text = "" + playerScript.myTrans.gpa;
        coursesDone.text = "" + playerScript.myTrans.coursesFinished;
        Score.text = "" + finalScore;



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
