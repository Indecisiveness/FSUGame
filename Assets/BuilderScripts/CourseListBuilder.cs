using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CourseListBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {

		CourseList MyCourseList = (CourseList) ScriptableObject.CreateInstance("CourseList");

		List<Object> AllCourses = new List<Object> ();

		AllCourses.AddRange(Resources.LoadAll ("Course", typeof(Course)));

		AllCourses.ForEach (x => MyCourseList.myCourses.Add ((Course)x));

		AssetDatabase.CreateAsset (MyCourseList, "Assets/Resources/MyCourseList.asset");


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
