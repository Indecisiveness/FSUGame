using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownPopulator : MonoBehaviour {

	public Dropdown RequirementSelector;

	public List<Dropdown> CourseChoosers = new List<Dropdown> (4);

	public List<string> CourseLocations = new List<string>();

	public List<bool> genReq;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Populate (){

		RequirementSelector.ClearOptions();
		List<string> NeedToTake = new List<string> ();
		genReq = new List<bool> ();

		//start new lists

		NeedToTake.Add ("Select a requirement"); //default option

		PlayerScript myPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		Transcript MyScript = myPlayer.myTrans;
		List<Course> coursesTaken = MyScript.coursesTaken;
		List<Course> myCourses = MyScript.coursesRequired;

		myCourses.ForEach (x => {
			if (x.CanTake(coursesTaken)){
				NeedToTake.Add(x.courseName);
				CourseLocations.Add("Course/" + x.name);
				genReq.Add(false);

			}
		});
		MyScript.genRequired.ForEach (x => {
			bool aCourse = false;
			int i = 0;
			while (!aCourse && i < x.availCourse.Count){
				aCourse = x.availCourse[i].CanTake(coursesTaken);
				i++;
				}
			if (aCourse){
				NeedToTake.Add (x.reqName);
				CourseLocations.Add("GenReq/" + x.name);
				genReq.Add(true);
			}
		});

		MyScript.GenEdReqs.ForEach(x => {
			bool aCourse = false;
			int i = 0;
			while (!aCourse && i < x.availCourse.Count){
				aCourse = x.availCourse[i].CanTake(coursesTaken);
				i++;
			}
			if (aCourse){
				NeedToTake.Add (x.reqName);
				CourseLocations.Add("GenReq/" + x.name);
				genReq.Add(true);
			}
		});
		RequirementSelector.AddOptions (NeedToTake);
		RequirementSelector.interactable = true;
	}

	public void PopulateOthers(){
		CourseChoosers.ForEach(x => x.BroadcastMessage("PopulateCourses"));

	}

}
