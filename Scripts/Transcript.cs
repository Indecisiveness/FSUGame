using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcript : ScriptableObject {



	public List<Course> coursesTaken = new List<Course>(); // initializes @ zero unless we decide a student can enter with AP credit
	public List<Course> coursesRequired = new List<Course>(); // needs to match major list
	public List<GenReq> genRequired = new List<GenReq>();//gen ed and major non-courses

	public List<Course> CoursesInProgress = new List<Course> ();
	public List<float> GradesInProgress = new List<float> ();

	public int coursesAttempted = 0;

	public bool readyForGrad = false; // initialize to false
	public string MajorName = "";
	public float gpa = 0.0f;

			

	public void TakeCourse(Course toTake)
	{
		if (!toTake.CanTake(coursesTaken))  // checks if course can be taken, if not breaks out of method
		{
			Debug.Log("Pre-reqs not met");

			return;
		}
				
		CoursesInProgress.Add (toTake);  //adds a course in progress
		GradesInProgress.Add (0);    //adds a grade
				

	}
		
	public void CompleteSemester() {
		int i = 0;
		float gpaWeight = gpa * coursesTaken.Count;
		float semesterWeight = 0;

		CoursesInProgress.ForEach (x => {
			coursesAttempted += 1;
			semesterWeight += GradesInProgress [i];
			if (GradesInProgress [i] >= 2) {
				coursesRequired.Remove (x);
				coursesTaken.Add (x);

				int j = 0;

				bool rem = false;

				while (j < genRequired.Count) {
					if (genRequired [j].isCourse (x)) {
						rem = true;
						break;
					}
					j++;
				}

				if (rem) {
					genRequired.RemoveAt (j);
				} 
			}
		});


		gpa = (gpaWeight + semesterWeight)/coursesAttempted;

			if (coursesRequired.Count == 0 && genRequired.Count == 0)
			{
				readyForGrad = true;
			}

		}



			public void ShowRemaining ()
			{
				foreach (Course courseLeft in coursesRequired)
				{
			Debug.Log(courseLeft.courseName);
				}
				foreach (GenReq reqLeft in genRequired)
				{
			Debug.Log(reqLeft.reqName);
				}
			}

			public void ChangeMajor(Major toBecome, Major genEds)
			{
				coursesRequired = toBecome.courseList;               //Change required courses to the new list
				foreach (Course needed in coursesRequired)
				{
					Course a = coursesTaken[0];
					int i = 0;
					while (needed != a && i++ < coursesTaken.Count)
					{
						a = coursesTaken[i];
					}
					if (needed == a)
					{
						coursesRequired.Remove(needed);
					} 

				}

				int x = toBecome.genList.Count + genEds.genList.Count;  //starting here, make a new list from the two existing lists

				List<GenReq> temp = new List<GenReq> (x);

				temp.AddRange (toBecome.genList);
				temp.AddRange (genEds.genList);

				genRequired = temp;

				foreach (GenReq needed in genRequired)
				{
					bool q = false;
					int i = 0;
					while (!q && i++ < coursesTaken.Count)
					{
						q = needed.isCourse (coursesTaken [i]);
					}
					if (q){
						genRequired.Remove(needed);
					}
				}
			}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
