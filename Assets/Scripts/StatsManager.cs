using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Orignal Author: Mark Roberts
/// Created: 17 March 2018
/// Function: This class manages the objects required for status display.
/// 
/// </summary>
public class StatsManager : MonoBehaviour {

	public StatsDisplay player1Stats;
	public StatsDisplay p2StatsGroup;
	public StatsDisplay p3StatsGroup;
	public StatsDisplay p4StatsGroup;
	private bool p2Joined;
	private bool p3Joined;
	private bool p4Joined;

	public AllNetworkedStats allStats;

	public List<float> GPAs;
	public List<string> standings;
	public List<string> playerNames;

	/// <summary>
	/// Upon start this method sets the default values to be 
	/// displayed on the status screen. 
	/// </summary>
	void Start () {

		PlayerScript pScript= GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		int myPNum = pScript.pNum;


		player1Stats.fillFields ();

		//the values should be set from characterStats?
	
	
		p2Joined = false;
		p3Joined = false;
		p4Joined = false;

		p2StatsGroup.gameObject.SetActive (false);
		p3StatsGroup.gameObject.SetActive (false);
		p4StatsGroup.gameObject.SetActive (false);

		NetworkManagerScript myNetwork = GameObject.FindObjectOfType<NetworkManagerScript> ();

		if (myNetwork != null) {
			allStats = myNetwork.GetComponentInChildren<AllNetworkedStats> ();
		

			if (allStats.gpas.Count > 1) {
				p2Joined = true;
			}
			if (allStats.gpas.Count > 2) {
				p3Joined = true;
			}
			if (allStats.gpas.Count > 3) {
				p4Joined = true;
			}

		}

	//These values need to be set based on number of players during multi-player game

		for (int i = 0; i < allStats.playerID.Count; i++) {
			if (myPNum != allStats.playerID [i]) {
				GPAs.Add (allStats.gpas [i]);
				standings.Add (allStats.standings [i]);
				playerNames.Add (allStats.pNames [i]);
			}
		}



		//call method to set visibility
		SetPlayerVisibility (p2StatsGroup, p2Joined, 0);
		SetPlayerVisibility (p3StatsGroup, p3Joined, 1);
		SetPlayerVisibility (p4StatsGroup, p4Joined, 2);
	}
	
	/// <summary>
	/// Sets the player visibility.
	/// </summary>
	/// <param name="statsGroup">Stats group.</param>
	/// <param name="joined">If set to <c>true</c> joined.</param>
	void SetPlayerVisibility(StatsDisplay statsGroup, bool joined, int playPos){
		if (joined) {
			statsGroup.gameObject.SetActive (true);
			statsGroup.GPA.text = "" + GPAs[playPos];
			statsGroup.classStanding.text = "" + standings[playPos];
			statsGroup.playerName.text = "" + playerNames[playPos];
		} else {
			statsGroup.gameObject.SetActive (false);
		}
	}
}
