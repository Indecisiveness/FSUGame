using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownPopulator : MonoBehaviour {

	public Dropdown RequirementSelector;

	public List<Dropdown> CourseChoosers = new List<Dropdown> (4);

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Populate (){

		RequirementSelector.ClearOptions();
		List<string> NeedToTake = new List<string> ();

		Transcript MyScript = Resources.Load<Transcript>("myTranscript");
		List<Course> myCourses = MyScript.coursesRequired;
		myCourses.ForEach (x => NeedToTake.Add(x.courseName));
		MyScript.genRequired.ForEach (x => NeedToTake.Add (x.reqName));
		MyScript.GenEdReqs.ForEach (x => NeedToTake.Add (x.reqName));


		RequirementSelector.AddOptions (NeedToTake);




		RequirementSelector.interactable = true;
	}

	public void PopulateOthers(){
		CourseChoosers.ForEach(x => x.BroadcastMessage("PopulateCourses"));

	}

}
