using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SemesterStart : MonoBehaviour {

	public List<ClassChooser> myCourses;



	public void SemesterBegin (){
		GameObject MyPlayer = GameObject.Find ("Player1");

		Transcript MyScript = Resources.Load<Transcript>("myTranscript");

		myCourses.ForEach (x => {
			if (x.CourseChosen) {
				MyScript.CoursesInProgress.Add (x.SelectedCourse);
			}
		});
	}	


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
