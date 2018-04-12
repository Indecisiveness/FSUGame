using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MajorListBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MajorList MyMajorList = (MajorList) ScriptableObject.CreateInstance("MajorList");

		List<Object> AllReqs = new List<Object> ();

		AllReqs.AddRange(Resources.LoadAll ("Major", typeof(Major)));

		AllReqs.ForEach (x => MyMajorList.MyMajors.Add ((Major)x));

		AssetDatabase.CreateAsset (MyMajorList, "Assets/Resources/MyMajorList.asset");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
