using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour {

	public bool HasAC=false;            // shows whether  AC marker is active
	public bool HasSC=false;            // shows whether  SC marker is active
    public bool HasWC=false;            // shows whether  WC marker is active

    public GameObject ACMark;           // Object to store AC marker reference
    public GameObject SCMark;           // Object to store SC marker reference
    public GameObject WCMark;           // Object to store WC marker reference

    string objectName = "";             // holds the name of the object for debugging purposes (From UnityEditor)
    int dayNumber = 00;                 // is the day# of the dayBox array 1-42 (from GameBoard)
    int month = 00;                     // is the current month number 1-12 (from GameBoard)


    public GameboardScript gameboardScript;     // object holding reference to the GameBoardScript
    public GameObject months;                   // object holding reference to the Gameboard itself

    PlayerScript playerScript;                  // object holding reference to PlayerScript
       

    // Use this for initialization
    void Start () {

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        months = GameObject.Find("Months");
        gameboardScript = months.GetComponent<GameboardScript>();

        objectName = gameObject.name;
        month = gameboardScript.currentMonth;                                               // sets dayNumber
        Int32.TryParse(objectName.Substring(4 , 2), out dayNumber);                         // sets dayNumber

        Debug.Log(objectName + " : " + objectName.Length + " my dayNumber is " + dayNumber);

        ACMark = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;         // get MarkerGroup --> ACMarker
        SCMark = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;         // get MarkerGroup --> SCMarker
        WCMark = transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;         // get MarkerGroup --> WCMarker

        updateMarkers();                                                                    // check updateMarkers method

    }
	
	// Update is called once per frame
	void Update () {

		if (gameboardScript.currentDayBox == dayNumber && gameboardScript.currentDayBox%7 != 1) {

            drawACCard();
            drawSCCard();
            drawWCCard();
        }
	}


    /** This method randomizes which markers to activate and deactivate.
     */
    void randomMarkers() {

        int acOn;           // 0 through 5                                                                                         
        int scOn;           // 0 through 5                             
        int wcOn;           // 0 through 5

        acOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f,6f));       // randomly generates between 0 and 5
        scOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 6f));     
        wcOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 6f));     


		//becomes true 1/6 of the time
        if (acOn == 5) { ACMark.SetActive(true); HasAC = true; playerScript.saveMarkers[(dayNumber * 3) - 2] = 1; } else { ACMark.SetActive(false); HasAC = false; playerScript.saveMarkers[(dayNumber * 3) - 2] = 0; }   // set the card marker for this day active based on 1 or 0
        if (scOn == 5) { SCMark.SetActive(true); HasSC = true; playerScript.saveMarkers[(dayNumber * 3) - 1] = 1; } else { SCMark.SetActive(false); HasSC = false; playerScript.saveMarkers[(dayNumber * 3) - 1] = 0; }   // set the card marker for this day active based on 1 or 0
        if (wcOn == 5) { WCMark.SetActive(true); HasWC = true; playerScript.saveMarkers[(dayNumber * 3) - 0] = 1; } else { WCMark.SetActive(false); HasWC = false; playerScript.saveMarkers[(dayNumber * 3) - 0] = 0; }   // set the card marker for this day active based on 1 or 0
    }


    /** This method puts zeroes in the saveMarkers array in the position of all 3 markers on this day when necessary
     */
    public void zeroOutSaveMarkers() {

		//remove all markers from the current day

        playerScript.saveMarkers[(dayNumber * 3) - 2] = 0;
        playerScript.saveMarkers[(dayNumber * 3) - 1] = 0;
        playerScript.saveMarkers[(dayNumber * 3) - 0] = 0;
    }


    /** This method has the day object itself check if it should make itself active or inactive.
     */
    public void amIValid() {
        
		//get rid of markers on days that don't exist

        if (dayNumber < gameboardScript.currentDayBox) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }

        if (month == 1 && dayNumber > 32) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 2 && dayNumber > 35) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 3 && dayNumber > 35) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 4 && dayNumber > 33) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 5 && dayNumber > 36) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 6 && dayNumber > 29) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 7 && dayNumber > 31) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 8 && dayNumber > 33) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 9 && dayNumber > 36) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 10 && dayNumber > 30) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 11 && dayNumber > 33) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }
        else if (month == 12 && dayNumber > 35) { zeroOutSaveMarkers(); gameObject.SetActive(false);  }

        checkMyMarkers();
       
        
    }

    public void checkMyMarkers() {

		//Turn on ACMarks

        if (playerScript.saveMarkers[(dayNumber * 3) - 2] == 1) { ACMark.SetActive(true); HasAC = true; } else { ACMark.SetActive(false); HasAC = false; }
        if (playerScript.saveMarkers[(dayNumber * 3) - 1] == 1) { SCMark.SetActive(true); HasSC = true; } else { SCMark.SetActive(false); HasSC = false; }
        if (playerScript.saveMarkers[(dayNumber * 3) - 0] == 1) { WCMark.SetActive(true); HasWC = true; } else { WCMark.SetActive(false); HasWC = false; }
    }

    public void updateMarkers() {

		//randomize markers

        if (gameboardScript.newMonth == true)       // if it is a new month then
        {
            randomMarkers();                // randomize which of the child markers of this day are active
            amIValid();                     // make yourself inactive if you're not valid (DayBox) along with all your markers (children)
        }
        else
        {                              // if not a new month
            checkMyMarkers();          // check  which of your markers should be on
        }
    }

    /**
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

*/

	//Draw all cards
    public void drawCards() { drawACCard();drawSCCard();drawWCCard(); }


	//draw a specific card
    public void drawACCard() {


        if (HasAC) {
            HasAC = false;
            gameboardScript.showRandomAcademic();
            ACMark.SetActive(false);
            playerScript.saveMarkers[(dayNumber * 3) - 2] = 0;
            return;
        }
    }

    public void drawSCCard() {

        if (HasSC) {
            HasSC = false;
            gameboardScript.showRandomSocial();
            SCMark.SetActive(false);
            playerScript.saveMarkers[(dayNumber * 3) - 1] = 0;
            return;
        }
    }

    public void drawWCCard()
    {
        if (HasWC)
        {
            HasWC = false;
            gameboardScript.showRandomWork();
            WCMark.SetActive(false);
            playerScript.saveMarkers[(dayNumber * 3) - 0] = 0;
            return;
        }
    }


       

	
    
}
