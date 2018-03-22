using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TranscriptBuilder : MonoBehaviour {


	public static Transcript Create(Major MyMajor, Major MyGenEds){
		Transcript myTranscript = ScriptableObject.CreateInstance<Transcript> ();

		myTranscript.genRequired = MyGenEds.genList;
		myTranscript.genRequired.AddRange(MyMajor.genList);
		myTranscript.coursesRequired = MyMajor.courseList;
		myTranscript.MajorName = MyMajor.MajorName;

		AssetDatabase.CreateAsset (myTranscript, "Assets/myTranscript.asset");
		AssetDatabase.SaveAssets();
		return myTranscript;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
