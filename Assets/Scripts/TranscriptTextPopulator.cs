using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranscriptTextPopulator: MonoBehaviour {

	public Transcript myTrans;
	public Text CurrentClasses;
	public Text CurrentGPAs;
	public Text CompleteClasses;
	public Text CumulativeGPA;



	// Use this for initialization
	void Start () {
		List<string> ClassNow = new List<string> ();
		myTrans.CoursesInProgress.ForEach (x => ClassNow.Add (x.courseName));

		List<string> GradeNow = new List<string> ();
		myTrans.GradesInProgress.ForEach (x => GradeNow.Add (x.ToString()));

		List<string> ClassTaken = new List<string> ();
		myTrans.coursesTaken.ForEach (x => ClassTaken.Add (x.courseName));

		string AllCNow = "";
		ClassNow.ForEach (x => AllCNow += x + "\r\n");

		CurrentClasses.text = AllCNow;

		string AllGNow = "";
		GradeNow.ForEach (x => AllGNow += x + "\r\n");

		CurrentGPAs.text = AllGNow;

		string AllCTake = "";
		ClassTaken.ForEach (x => AllCTake += x + "\r\n");

		CompleteClasses.text = AllCTake;

		CumulativeGPA.text = myTrans.gpa.ToString();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
