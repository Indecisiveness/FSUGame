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

	public NewGBScript MyGameboard;


	// Use this for initialization
	void Start () {
        MyGameboard = GameObject.Find("FallGB").GetComponent<NewGBScript>();   //GetComponentInParent<NewGBScript>();
		ACMark.SetActive (false);
		SCMark.SetActive (false);
		WCMark.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void WeekStart(){

		double AC = Random.value;
		if (AC> .86){
			this.HasAC = true;
			this.ACMark.SetActive(true);
		}
		double SC = Random.value;
		if (SC> .86){
			this.HasSC = true;
			this.SCMark.SetActive(true);
        }
		double WC = Random.value;
		if (WC> .86){
			this.HasWC = true;
			this.WCMark.SetActive(true);
        }

		UpdateMarks ();

	}

	public void UpdateMarks(){
		if (HasAC) {
			ACMark.SetActive (true);
		} else {
			ACMark.SetActive (false);
		}

		if (HasSC) {
			SCMark.SetActive (true);
		} else {
			SCMark.SetActive (false);
		}

		if(HasWC){
			WCMark.SetActive (true);
		} else {
			WCMark.SetActive (false);
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
