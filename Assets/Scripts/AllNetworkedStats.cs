using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AllNetworkedStats : NetworkBehaviour {

	[SyncVar]
	public SyncListFloat gpas = new SyncListFloat();

	[SyncVar]
	public SyncListString standings = new SyncListString();

	[SyncVar]
	public SyncListInt playerID= new SyncListInt();

	[SyncVar]
	public SyncListString pNames = new SyncListString();


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetPlayStats(){
		if (isServer) {

			GameObject myParent = gameObject.transform.parent.gameObject;

			NetworkedStatsScript[] allPlayStats = myParent.GetComponentsInChildren<NetworkedStatsScript> ();



			for (int i = playerID.Count; i < allPlayStats.Length; i++) {
				gpas.Add(allPlayStats [i].myGPA);
				standings.Add(allPlayStats [i].myStanding);
				playerID.Add(allPlayStats [i].playerID);
				pNames.Add(allPlayStats [i].pName);
			}
		}

	}

	public void UpdateStats(int pID, float myGPA, string myStanding, string myName){

		if (isServer) {
			for (int i = 0; i < playerID.Count; i++) {
				if (playerID [i] == pID) {
					gpas [i] = myGPA;
					standings [i] = myStanding;
					pNames [i] = myName;
				}
			}
		}


	}


}
