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
		player1Stats.playerName.text = "Bobo the Player";
		player1Stats.jobSkills.text = "00";
		player1Stats.studyHabits.text = "01";
		player1Stats.socialSkills.text = "02";
		player1Stats.physicalHealth.text = "03";
		player1Stats.sanity.text = "04";
		player1Stats.motivation.text = "05";
		player1Stats.finances.text = "06";
		player1Stats.GPA.text = "07";
		player1Stats.classStanding.text = "Fr.";
		player1Stats.currentYear.text = "1st";
		player2Stats.playerName.text = "KoKOMo";
		player2Stats.GPA.text = "08";
		player2Stats.classStanding.text = "So.";
		player2Stats.currentYear.text = "2nd";
		player3Stats.playerName.text = "Biggie 33";
		player3Stats.GPA.text = "09";
		player3Stats.classStanding.text = "Jr.";
		player3Stats.currentYear.text = "3rd";
		player4Stats.playerName.text = "Last 4";
		player4Stats.GPA.text = "10";
		player4Stats.classStanding.text = "Sr.";
		player4Stats.currentYear.text = "4th";

	//These values need to be set based on number of players during multi-player game
		p2Joined = true;
		p3Joined = true;
		p4Joined = true;

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
