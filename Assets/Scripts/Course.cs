//Class for the Course object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : ScriptableObject{

	public string courseName = ""; // full name

	public List<string> preRequisites = new List<string>();// get passed into constructor from .csv program that auto generates



	public bool CanTake (List<Course> taken) // Course[] taken is from the transcript
	{
		foreach (string course in preRequisites) {  // loop through prereqs

			string a = ""; // Stored courseName from each course taken
			int i = 0;
			while (a != course && i < taken.Count) { // checks prereq name vs. names in transcript
				a = taken[i].name;
				i++;
			}
			if (a != course) {
				return false;
			}
		}
		return true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
