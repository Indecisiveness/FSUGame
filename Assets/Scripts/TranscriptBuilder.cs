using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptBuilder : MonoBehaviour {


	public static Transcript Create(Major MyMajor, Major MyGenEds){
		Transcript myTranscript = Resources.Load<Transcript> ("myTranscript");

		myTranscript.genRequired = MyGenEds.genList;
		myTranscript.genRequired.AddRange(MyMajor.genList);
		myTranscript.coursesRequired = MyMajor.courseList;
		myTranscript.MajorName = MyMajor.name;

		return myTranscript;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
