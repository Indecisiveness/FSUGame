using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MajorPicker : MonoBehaviour {

	public Dropdown MyDropdown;

	// Use this for initialization
	void Start () {
	
		MajorList AllMajors = Resources.Load ("AllMajors") as MajorList;

		List<string> myNames = new List<string> ();

		AllMajors.MyMajors.ForEach(x => myNames.Add(x.name));

		MyDropdown.ClearOptions ();

		MyDropdown.AddOptions (myNames);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
