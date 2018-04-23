using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedStatsScript : NetworkBehaviour {
    

  
    public float myGPA = 4.0f;            
    public string myStanding = "Senior";

	[SyncVar]
	public int playerID = -1;

	public string pName = "???";

	public AllNetworkedStats myNetStats;


    // Use this for initialization
    void Start () {
        this.gameObject.transform.parent = GameObject.Find("NetworkManager").transform; 
		CmdPlayerNum ();
    }
	
	// Update is called once per frame
	public void StatusUpdate () {
		if (!isLocalPlayer){
			return;
			}

		GameObject myPlayer = GameObject.FindGameObjectWithTag("Player");
		PlayerScript myScript = myPlayer.GetComponent<PlayerScript> ();

		float oldGPA = myGPA;
		string oldStanding = myStanding;

		myGPA = myScript.myTrans.gpa;
		myStanding = myScript.myTrans.classStanding;

		pName = myScript.charName;

		if (oldGPA != myGPA || oldStanding != myStanding) {
			Debug.Log ("changed a stat");


			CmdUpdateStats(playerID, myGPA, myStanding, pName);
			myScript.pNum = playerID;

		
		}

	}

    public void testSyncVars() {
		Debug.Log ("Calling testSyncVars on NetworkedStatsScript");
	}


	[Command]
	public void CmdUpdateStats(int pID, float GPA, string standing, string pName){

		myNetStats = gameObject.transform.parent.GetComponentInChildren<AllNetworkedStats> ();
		myNetStats.UpdateStats (pID, GPA, standing, pName);
	}

	public void CmdPlayerNum(){
		int newPNum = this.transform.parent.gameObject.GetComponent<NetworkManagerScript> ().pNum ();
		playerID = newPNum;
	}
}
