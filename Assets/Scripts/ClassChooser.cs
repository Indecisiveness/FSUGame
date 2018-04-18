//Class for dropdowns that show course selections at the start of each semester

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassChooser : MonoBehaviour {

	public Dropdown requirementSelector;  //the dropdown that picks a req

	public Dropdown mySelf; 

	public GameObject pickedCourses; //course or genreq picked on the main dropdown

	public List<Course> currentOptions;//options as courses

	public Course SelectedCourse;//most recently selected course

	public bool CourseChosen;//has a course been chosen?

	// Use this for initialization
	void Start () {
		CourseChosen = false;
		mySelf = gameObject.GetComponent<Dropdown> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
	public void PopulateCourses(){

		//get course options
		PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		Transcript myScript = playerScript.myTrans;



		int MyValue = requirementSelector.value -1;

		if (MyValue < 0) {//if nothing has been selected on the requirement selector, don't populate
			return;
		}



		mySelf.ClearOptions (); 

		//the following get the courses GenReq from the resources folder and stores the relevant ones

		DropdownPopulator myDrop = requirementSelector.GetComponent<DropdownPopulator>();

		string location = myDrop.CourseLocations [MyValue];

		bool genReq = myDrop.genReq [MyValue];

		List<string> MyCourses = new List<string>();

		if (CourseChosen) {
			MyCourses.Add (SelectedCourse.courseName);
		} else {
			MyCourses.Add ("Select a course");
		}
			
		if (genReq) {
			GenReq myReq = Resources.Load<GenReq> (location);
			currentOptions = myReq.availCourse;

			currentOptions.ForEach (x => {
				if (x.CanTake(myScript.coursesTaken)){
					MyCourses.Add (x.courseName);
				}
			});
		} else {
			Course thisCourse = Resources.Load<Course>(location);
			currentOptions = new List<Course> {thisCourse};
			MyCourses.Add (currentOptions[0].courseName);
		}



		mySelf.AddOptions (MyCourses);	//add the options to the dropdown menu

		if (CourseChosen) {
				mySelf.value = 0; //set the dropdown to show the previously selected option
		}
	}

	public void CourseChose(){ //Function to save the course picked
		if (mySelf.value > 0) {
			SelectedCourse = currentOptions[mySelf.value - 1];  
			CourseChosen = true;
		}
	}
    
     
}
