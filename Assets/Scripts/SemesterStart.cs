//Class on the course selection screen that progresses to the start of the semester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SemesterStart : MonoBehaviour {

	public List<ClassChooser> myCourses;



	public void SemesterBegin (){

		Transcript MyScript = Resources.Load<Transcript>("myTranscript");
		MyScript.CoursesInProgress = new List<Course>();
		MyScript.GradesInProgress = new List<float> ();

		myCourses.ForEach (x => {   //put each course onto the "taken" part of the transcript and start with Cs in all
			if (x.CourseChosen) {
				MyScript.CoursesInProgress.Add (x.SelectedCourse);
				MyScript.GradesInProgress.Add (2f);      
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
