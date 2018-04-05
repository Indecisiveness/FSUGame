using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {

	public bool HasAC=false;
	public bool HasSC=false;
	public bool HasWC=false;

	public GameObject ACMark;
	public GameObject SCMark;
	public GameObject WCMark;

	public GameboardScript MyGameboard;


	// Use this for initialization
	void Start () {
		MyGameboard= GetComponentInParent<GameboardScript>();
		Transform Location = gameObject.transform.Find("MarkerGroup");

		ACMark = Location.Find("AC").gameObject;
		SCMark = Location.Find("SC").gameObject;	
		WCMark = Location.Find("WC").gameObject;
		ACMark.SetActive (false);
		SCMark.SetActive (false);
		WCMark.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void WeekStart(){

		if (HasAC){
			ACMark.SetActive (true);
		}

		if(HasSC){
			SCMark.SetActive(true);
		}

		if(HasWC){
			WCMark.SetActive (true);
		}
	}


	public void DrawCard(){


		if (HasAC) {
			HasAC = false;
			MyGameboard.showRandomAcademic();
			ACMark.SetActive (false);
			return;
		} else if (HasSC) {
			HasSC = false;
			MyGameboard.showRandomSocial();
			SCMark.SetActive (false);
			return;
		} else if (HasWC) {
			HasWC = false;
			MyGameboard.showRandomWork();
			WCMark.SetActive (false);
			return;
		}

	}

}
