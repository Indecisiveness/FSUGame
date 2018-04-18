//Class for Majors, as well as the class used for the general requirements that all majors must meet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Major : ScriptableObject{


	public string MajorName
	{ get; set; }
		
	public List<Course> courseList = new List<Course> (); //List of courses required
	public List<GenReq> genList = new List<GenReq> ();

			

		

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
