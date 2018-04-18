//Function to show and hide the help text and help button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour {
	
	public GameObject helpObjects;
	public GameObject helpButton;

	public void show(){
		helpObjects.SetActive (true);
		helpButton.SetActive (false);
	}


	public void hide(){
		helpObjects.SetActive (false);
		helpButton.SetActive (true);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
