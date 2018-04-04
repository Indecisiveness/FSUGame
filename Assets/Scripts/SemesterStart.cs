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

		myCourses.ForEach (x => {
			if (x.CourseChosen) {
				MyScript.CoursesInProgress.Add (x.SelectedCourse);
				MyScript.GradesInProgress.Add (0);
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
