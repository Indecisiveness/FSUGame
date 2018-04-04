using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public StatsDisplay player1Stats;
	public StatsDisplay player2Stats;
	public StatsDisplay player3Stats;
	public StatsDisplay player4Stats;
	public GameObject p2StatsGroup;
	public GameObject p3StatsGroup;
	public GameObject p4StatsGroup;
	private bool p2Joined;
	private bool p3Joined;
	private bool p4Joined;

	// Use this for initialization
	void Start () {

		//the values should be set from characterStats?
	
		player1Stats.classStanding.text = "Fr.";
		player1Stats.currentYear.text = "1st";


	//These values need to be set based on number of players during multi-player game
		p2Joined = false;
		p3Joined = false;
		p4Joined = false;

		//call method to set visibility
		SetPlayerVisibility (p2StatsGroup, p2Joined);
		SetPlayerVisibility (p3StatsGroup, p3Joined);
		SetPlayerVisibility (p4StatsGroup, p4Joined);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// This method sets visibility to player 2,3 & 4 stats group
	void SetPlayerVisibility( GameObject statsGroup, bool joined){
		if (joined) {
			statsGroup.SetActive (true);
		} else {
			statsGroup.SetActive (false);
		}
	}
}
