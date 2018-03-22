using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using UnityEngine.UI;
using UnityEditor;

public class MajorBuilder : MonoBehaviour {



	public Dropdown myDropdown;

	public List<string> MajorNameList;

	public TextAsset MyMajorText;

	// Use this for initialization
	public void BuildMajors () {

		CourseList myCourseList = Resources.Load<CourseList>("Resources/allCourses.asset");
		GenReqList MyGenReqList = Resources.Load<GenReqList> ("Resources/allCourses.asset");

		MajorList MyMajorList = ScriptableObject.CreateInstance<MajorList>();

		try{  //trycatch

			{
				string AllLines = MyMajorText.text;
				List<string> lines = new List<string>(AllLines.Split(new char[] {'\n', '\r'}));  //retrieve each line
				for (int i = 0; i<lines.Count; i++){
					List<string> line = new List<string> (lines[i].Split(':'));  //retrieve each line as a list of strings, splitting at :
					List<List<string>> MyMajorInfo= new List<List<string>>(); //create empty list of lists of strings
					line.ForEach( x => MyMajorInfo.Add(new List<string>(x.Split(','))));  //for each string in line, split at , and put list into list of lists

					string MyMajorName = MyMajorInfo[0][0];//retrieve first item in first list, name

					Major AMajor = ScriptableObject.CreateInstance<Major>();  //new major object
					AMajor.MajorName = MyMajorName;   //set MajorName property
					AMajor.name = MyMajorName;      //set name object property

					MyMajorInfo[1].ForEach(x => {   //for each string from the text file...
						Course ThisCourse = Resources.Load<Course>("Course/" + x);//...find the matching course by name
						AMajor.courseList.Add(ThisCourse);  //Add this course to the major's course list
					});

					List<string> GenEdnames = new List<string>(MyMajorInfo[2]);

					GenEdnames.ForEach(x=> {
						GenReq ThisReq = Resources.Load<GenReq>("GenReq/"+x);
						AMajor.genList.Add(ThisReq);
					});
						
					MyMajorList.MyMajors.Add(AMajor);
					AssetDatabase.CreateAsset(AMajor,"Assets/Resources/Major/"+AMajor.MajorName+".asset");
					i++;
				}
			}
		MyMajorList.MyMajors.ForEach(x => MajorNameList.Add(x.MajorName));


			AssetDatabase.CreateAsset(MyMajorList, "Assets/Resources/AllMajors.asset");
		}
		catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}

		myDropdown.ClearOptions();
		myDropdown.AddOptions (MajorNameList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
