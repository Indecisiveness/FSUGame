//Function for creating card effects during gameplay

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public int stat1;
	public int lowbound1;
	public int highbound1;

	public int stat2;
	public int lowbound2;
	public int highbound2;

	/* Set bounds to 100 to avoid using them. Stats are JobSkills, StudyHabits, SocialSkills, PhysicalHealth, Sanity,
	 * Motivation, Finances, WorkTime, StudyTime, SocialTime, 
	 * total of 10 modifiers can affect 12 counting CourseGradeSelected, CourseGradeRandom */

	public List<int> myMod;


	public List<int> lowlow = new List<int>(12);
	public List<int> midlow = new List<int>(12);
	public List<int> highlow = new List<int>(12);
	public List<int> lowmid = new List<int>(12);
	public List<int> midmid = new List<int>(12);
	public List<int> highmid = new List<int>(12);
	public List<int> lowhigh = new List<int>(12);
	public List<int> midhigh = new List<int>(12);
	public List<int> highhigh = new List<int>(12);

	List<List<int>> modifiers;



	/*first list is low/low, second is med/low, third is high/low, 4th is low/med, 5th is med/med, 6th is high/med,
	 * 7th low/high, 8th mid/high, and 9th high/high
*/

	//public Button FromDeck;  //deck that card was drawn from

	GameObject player;
	PlayerScript playerScript;

	Transcript myTranscript;
	GameObject ApprovalPanel;


	// Use this for initialization
	void Start () {
		modifiers = new List<List<int>>{lowlow, midlow, highlow, lowmid, midmid, highmid, lowhigh, midhigh, highhigh};


	}
	
	                                                                
	void Update () {
		
	}



	public void GenerateEffect(){                                  //Generate Card effect
		
		List<int> myStats = new List<int> (10);

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerScript>();

		List<int> timeAllotment = new List<int> {(int)playerScript.timeWork,(int)playerScript.timeStudy,(int)playerScript.timeSocial}; //retrieve time spent

		myTranscript = Resources.Load<Transcript> ("MyTranscript");

		myStats.AddRange(playerScript.charStats);  //Adds first 7

		myStats.AddRange(timeAllotment);  //Adds next 3


		//the following determines which effect array to use
		int myOption = 0;

		if (lowbound1 <= myStats[stat1] && highbound1>myStats[stat1])
		{
			myOption = 1;
		}
		if (myStats [stat1] >= highbound1) {
			myOption = 2;
		}
		if (myStats [stat2] >= lowbound2 && highbound2 >= myStats [stat2]) {
			myOption += 3;
		}
		if (myStats [stat2] >= highbound2) {
			myOption += 6;
		}

		List<int> myMod = modifiers [myOption];


		//updates the list of stats
		int i = 0;

		myStats.ForEach (x => {
			myStats[i] = x + myMod [i];
			if (i < 7 && myStats[i] > 10){//stats capped at 10
				myStats[i] = 10;
			}
			i++;
		});

		playerScript.charStats = myStats.GetRange (0, 7);

		playerScript.timeWork = myStats [7];
		playerScript.timeStudy = myStats [8];
		playerScript.timeSocial = myStats [9];
			
		if (myMod [10] != 0) {

			int thisCourse = Random.Range (0, myTranscript.GradesInProgress.Count);
			myTranscript.GradesInProgress [thisCourse] += myMod [10];
			if (myTranscript.GradesInProgress [thisCourse] > 4) {
				myTranscript.GradesInProgress [thisCourse] = 4; //grade caps at 4
			}
		}



/*		if (myMod[11] !=0){

		List<string> myOptions = new List<string>{"Select a course"};

		myTranscript.CoursesInProgress.ForEach(x => myOptions.Add(x.courseName));

		GameObject PopUpSelector = Instantiate (ApprovalPanel);

		Dropdown CourseSelector = PopUpSelector.AddComponent<Dropdown> ();

		CourseSelector.ClearOptions ();
		CourseSelector.AddOptions (myOptions);
			CourseSelector.onValueChanged (ChangeGrade (CourseSelector.value - 1, myMod [11]));
		}
*/
	}




}


