using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("LevelManager working");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buttonRedirect(string s){

		Debug.Log ("Button Clicked: " + s);
		SceneManager.LoadScene(s);
	}


}
