using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassChooser : MonoBehaviour {

	public Dropdown RequirementSelector;

	public Dropdown MySelf;

	public List<Course> PickedCourses;

	public Course SelectedCourse;

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


		GameObject Play1 =  GameObject.FindGameObjectWithTag("Player");

		Transcript myTranscript = (Play1.GetComponent<PlayerScript> ()).myTrans;

		int MyValue = RequirementSelector.value;

		Dropdown ThisDropdown = gameObject.GetComponent<Dropdown> ();

		ThisDropdown.ClearOptions ();
		PickedCourses = new List<Course>();

		List<string> MyCourses;

		if (!CourseChosen) {
			MyCourses = new List<string> { "Select a Course" };
		} 
		else {
			MyCourses = new List<string> { SelectedCourse.courseName };
		}

		if (MyValue < myTranscript.coursesRequired.Count) 
			{
				if (myTranscript.coursesRequired[MyValue].CanTake(myTranscript.coursesTaken))
				{
					MyCourses.Add(myTranscript.coursesRequired[MyValue].name);
					PickedCourses.Add (myTranscript.coursesRequired[MyValue]);

				}
					ThisDropdown.AddOptions (MyCourses);
			}

			
		else if (MyValue - myTranscript.coursesRequired.Count < myTranscript.genRequired.Count) {
				List<Course> MyCourseList = myTranscript.genRequired[MyValue-myTranscript.coursesRequired.Count].availCourse;
				MyCourseList.ForEach ( x => {
						if (x.CanTake(myTranscript.coursesTaken)){
							MyCourses.Add(x.name);
							PickedCourses.Add(x);
						}
					});
				ThisDropdown.AddOptions (MyCourses);
		}

	}

	public void CourseChose(){
		if (MySelf.value > 0) {
			SelectedCourse = PickedCourses [MySelf.value - 1];
			CourseChosen = true;
		}
	}
    
     
}
