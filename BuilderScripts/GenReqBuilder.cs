using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEditor;


public class GenReqBuilder : MonoBehaviour {



	public MajorBuilder NextBuilder;

	public TextAsset GenEdText;


	// Use this for initialization
	public void BuildGenEds (CourseList MyCourseList) {

		try{  //trycatch
			string allLines = GenEdText.text;
			List<string> lines = new List<string>(allLines.Split(new char[] {'\n', '\r'})); 
			GenReqList AllGenReqList = ScriptableObject.CreateInstance<GenReqList> ();//Create Game Object

			for (int i = 0; i < lines.Count; i++){
				List<string> myGenEdInfo = new List<string>(lines[i].Split(',')); //separate PreReqs
				GenReq AGenReq = ScriptableObject.CreateInstance<GenReq>();// name is first word in each line

				AGenReq.reqName = myGenEdInfo[0];
				AGenReq.name = myGenEdInfo[0];
				myGenEdInfo.RemoveAt(0); 
				List<string> Coursenames = new List<string>(); //Create list of prereqs
				Coursenames.AddRange(myGenEdInfo);
				//Empty course list
				Coursenames.ForEach(x => {                          //for each string from the text file...
					Course tryCourse = (Resources.Load<Course>("Course/"+x));  //...find the matching course by name
					Debug.Log("add object " + tryCourse.courseName);
					AGenReq.availCourse.Add(tryCourse);
				});

				AGenReq.courseNames.AddRange(Coursenames);  //set courselist to genreq's available courses


				string path = "Assets/Resources/GenReq/" +AGenReq.reqName + ".asset";

				AssetDatabase.CreateAsset(AGenReq,path);

				AllGenReqList.MyGenReqs.Add(AGenReq);

				i++;
			}

			AssetDatabase.CreateAsset(AllGenReqList, "Assets/Resources/AllGenReqList.asset");


		}
		catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}

		NextBuilder.BuildMajors ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
