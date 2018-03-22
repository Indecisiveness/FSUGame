using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownPopulator : MonoBehaviour {

	public Dropdown RequirementSelector;

	public List<Dropdown> CourseChosers = new List<Dropdown> (4);

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Populate (){
		GameObject Play1 = GameObject.Find("Player1");

		RequirementSelector.ClearOptions ();
		List<string> NeedToTake = new List<string> ();

		Transcript MyScript = (Play1.GetComponent<PlayerScript>()).myTrans;
		List<Course> myCourses = MyScript.coursesRequired;
		myCourses.ForEach (x => NeedToTake.Add(x.courseName));
		MyScript.genRequired.ForEach (x => NeedToTake.Add (x.reqName));
		RequirementSelector.AddOptions (NeedToTake);
	}

	public void PopulateOthers(){
		CourseChosers.ForEach(x => x.BroadcastMessage("PopulateCourses"));

	}

}
