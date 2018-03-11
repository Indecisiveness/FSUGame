using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public string charName;                                        //char name
	public List<int> charStats = new List<int>(6);                 //empty list
	public List<string> statNames = new List<string>{"study", "work", "social", "health", "sanity", "motivation"};  //stat names

	// Use this for initialization
	void Start () {
		}
	
	// Update is called once per frame
	void Update () {           		
	}

	public void StayAlive () {
		DontDestroyOnLoad (gameObject);
	}
}
