using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;


public class CourseBuilder : MonoBehaviour {

	public TextAsset myCourses;

	public GenReqBuilder MyBuilder;

	// Use this for initialization
	void Start () {
		try{  //trycatch
			{
				string allLines = myCourses.text;
				List<string> lines = new List<string>(allLines.Split(new char[] {'\n', '\r'}));  //retrieve each line
					


				CourseList allCourses = ScriptableObject.CreateInstance<CourseList>();//create game object

				for (int i = 0; i < lines.Count; i++){
					List<string> myCourseInfo = new List<string>(lines[i].Split(',')); //separate PreReqs
					Course ACourse = ScriptableObject.CreateInstance<Course>();// name is first word in each line

					ACourse.courseName = myCourseInfo[0];
					ACourse.name = myCourseInfo[0];
					myCourseInfo.RemoveAt(0); //remove name from list



					List<string> MyPreReqs = new List<string>(myCourseInfo); //Create list of prereqs
					ACourse.preRequisites = MyPreReqs;//Assign prereqs to course


					AssetDatabase.CreateAsset(ACourse,"Assets/Resources/Course/"+ACourse.courseName+".asset");


					allCourses.myCourses.Add(ACourse);//store course in list of courses
					i++;//iterate to next index in course list
				}

				AssetDatabase.CreateAsset(allCourses, "Assets/Resources/allCourses.asset");

				CourseList MyAsset = Resources.Load<CourseList>("allCourses");

				MyBuilder.BuildGenEds (MyAsset);

			}
		}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}

		}
	
	// Update is called once per frame
	void Update () {
		
	}
}
