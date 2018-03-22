using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassChooser : MonoBehaviour {

	public Dropdown RequirementSelector;

	public bool CourseChosen;

	// Use this for initialization
	void Start () {
		CourseChosen = false;
		PopulateCourses ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PopulateCourses(){


		GameObject Play1 = GameObject.Find ("Player1");

		Transcript myTranscript = (Play1.GetComponent<PlayerScript> ()).myTrans;

		int MyValue = RequirementSelector.value;

		Dropdown ThisDropdown = gameObject.GetComponent<Dropdown> ();

		if (!CourseChosen)
		{
		
			ThisDropdown.ClearOptions ();

			List<string> MyCourses = new List<string> { "Select a Course" };

			if (MyValue < myTranscript.coursesRequired.Count) 
			{
				if (myTranscript.coursesRequired[MyValue].CanTake(myTranscript.coursesTaken))
				{
					MyCourses.Add(myTranscript.coursesRequired[MyValue].courseName);

				}
					ThisDropdown.AddOptions (MyCourses);
			}

			
			else if (MyValue - myTranscript.coursesRequired.Count < myTranscript.genRequired.Count) {
						List<Course> MyCourseList = myTranscript.genRequired[MyValue-myTranscript.coursesRequired.Count].availCourse;
						MyCourseList.ForEach ( x => {
							if (x.CanTake(myTranscript.coursesTaken)){
								MyCourses.Add(x.courseName);
							}
						}
						);
				ThisDropdown.AddOptions (MyCourses);
		}
		}

	}

	public void CourseChose(){
		CourseChosen = true;
	}
}
