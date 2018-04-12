using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptBuilder : MonoBehaviour {


	public static Transcript Create(Major MyMajor, Major MyGenEds){
		Transcript myTranscript = Resources.Load<Transcript> ("myTranscript");

		myTranscript.genRequired = MyMajor.genList;
		myTranscript.GenEdReqs = MyGenEds.genList;

		myTranscript.coursesRequired = MyMajor.courseList;
		myTranscript.MajorName = MyMajor.name;

		myTranscript.coursesAttempted = 0;
		myTranscript.CoursesInProgress = new List<Course>();
		myTranscript.coursesTaken = new List<Course>();
		myTranscript.gpa = 0;
		myTranscript.GradesInProgress = new List<float>();
		myTranscript.readyForGrad = false;

		return myTranscript;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
