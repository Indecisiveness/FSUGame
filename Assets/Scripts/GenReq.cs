//Class for the GenReqs from majors and the requirements for all majors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenReq : ScriptableObject {


	public string reqName = "";

	public List<Course> availCourse = new List<Course>(); //list of courses meeting requirement

	public List<string> courseNames = new List<string>(); //created from list of courses



	public bool isCourse (Course toCheck) {//Determines if a course matches a name
		foreach (string listed in courseNames) {
			if (toCheck.courseName == listed) {//Compare to each possible value
				return true;
			}
		}
		return false; //If fails to find, return false
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
