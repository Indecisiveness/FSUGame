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


    public NewGBScript gameboardScript;     // object holding reference to the GameBoardScript
    public GameObject months;                   // object holding reference to the Gameboard itself

    PlayerScript playerScript;                  // object holding reference to PlayerScript
    GameObject player;

    // Use this for initialization
    void Start()
    {
        //DY - commenting out -> player = GameObject.FindGameObjectWithTag("Player");
        //DY - commenting out -> playerScript = player.GetComponent<PlayerScript>();
        //DY - commenting out -> months = GameObject.Find("Months");
        gameboardScript = GameObject.Find("FallGB").GetComponent<NewGBScript>(); //Using newGBScript -> months.GetComponent<GameboardScript>();

        //DY - commenting out -> objectName = gameObject.name;
        //DY - commenting out -> month = gameboardScript.currentMonth;                                               // sets dayNumber
        //DY - commenting out -> Int32.TryParse(objectName.Substring(4, 2), out dayNumber);                         // sets dayNumber

        //DY - commenting out -> Debug.Log(objectName + " : " + objectName.Length + " my dayNumber is " + dayNumber);

        //ACMark = transform.GetChild(0).transform.GetChild(0).gameObject; //DY - commenting out -> transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;         // get MarkerGroup --> ACMarker
        //SCMark = transform.GetChild(0).transform.GetChild(1).gameObject; //DY - commenting out -> transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;         // get MarkerGroup --> SCMarker
        //WCMark = transform.GetChild(0).transform.GetChild(2).gameObject; //DY - commenting out -> transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;       // get MarkerGroup --> WCMarker



        //DY - commenting out -> updateMarkers();
        //DY - commenting out -> noSundays();
    }

    // Update is called once per frame
    void Update () {        
        /* DY Commenting out
        if (gameboardScript.currentDayBox == dayNumber) {

            drawCards();
        }
        DY*/
	}


    /** This method randomizes which markers to activate and deactivate.
     */
    public void randomMarkers() {

        int acOn;           // 0 to 5                                                                                          
        int scOn;           // 0 to 5                              
        int wcOn;           // 0 to 5

        acOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f,6f));       // randomly generate 0-5
        scOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 6f));      
        wcOn = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 6f));      

        if (acOn == 0) { ACMark.SetActive(true); HasAC = true; ACMark.GetComponent<SpriteRenderer>().sortingOrder = 0; } //DY - commenting out ->  playerScript.saveMarkers[(dayNumber * 3) - 2] = 1; }     // set the card marker for this day active based on 1 or 0
        else { ACMark.SetActive(false); HasAC = false;  } //DY - commenting out ->  playerScript.saveMarkers[(dayNumber * 3) - 2] = 0; }   
        if (scOn == 0) { SCMark.SetActive(true); HasSC = true; SCMark.GetComponent<SpriteRenderer>().sortingOrder = 0; } //DY - commenting out ->  playerScript.saveMarkers[(dayNumber * 3) - 1] = 1; }     // set the card marker for this day active based on 1 or 0
        else { SCMark.SetActive(false); HasSC = false; } //DY - commenting out ->  playerScript.saveMarkers[(dayNumber * 3) - 1] = 0; }   
        if (wcOn == 0) { WCMark.SetActive(true); HasWC = true; WCMark.GetComponent<SpriteRenderer>().sortingOrder = 0;  } //DY - commenting out ->  playerScript.saveMarkers[(dayNumber * 3) - 0] = 1; }     // set the card marker for this day active based on 1 or 0
        else { WCMark.SetActive(false); HasWC = false; } //DY - commenting out -> playerScript.saveMarkers[(dayNumber * 3) - 0] = 0; }   
    }

    public void turnOffDay() {

        //zeroOutSaveMarkers();
        ACMark.SetActive(false);
        SCMark.SetActive(false);
        WCMark.SetActive(false);
        HasAC = false;
        HasSC = false;
        HasWC = false;
    }

    public void noSundays() {

        if (dayNumber == 01 || dayNumber == 08 || dayNumber == 15 || dayNumber == 22 || dayNumber == 29 || dayNumber == 36)
        {

            turnOffDay();
        }
    }

    /** This method puts zeroes in the saveMarkers array in the position of all 3 markers on this day when necessary
     */
    public void zeroOutSaveMarkers() {

        playerScript.saveMarkers[(dayNumber * 3) - 2] = 0;
        playerScript.saveMarkers[(dayNumber * 3) - 1] = 0;
        playerScript.saveMarkers[(dayNumber * 3) - 0] = 0;
    }

    /*DY Commenting out
    public static explicit operator Day(GameObject v)
    {
        throw new NotImplementedException();
    }
    DY*/

    /** This method has the day object itself check if it should make itself active or inactive.
     */
    public void amIValid() {
        /* DY Commenting out
        if (dayNumber < gameboardScript.currentDayBox) { turnOffDay(); }        

        if (month == 1 && dayNumber > 32) { turnOffDay();  }
        else if (month == 2 && dayNumber > 35) { turnOffDay(); }
        else if (month == 3 && dayNumber > 35) { turnOffDay(); }
        else if (month == 4 && dayNumber > 33) { turnOffDay(); }
        else if (month == 5 && dayNumber > 36) { turnOffDay(); }
        else if (month == 6 && dayNumber > 29) { turnOffDay(); }
        else if (month == 7 && dayNumber > 31) { turnOffDay(); }
        else if (month == 8 && dayNumber > 33) { turnOffDay(); }
        else if (month == 9 && dayNumber > 36) { turnOffDay(); }
        else if (month == 10 && dayNumber > 30) { turnOffDay(); }
        else if (month == 11 && dayNumber > 33) { turnOffDay(); }
        else if (month == 12 && dayNumber > 35) { turnOffDay(); }

        noSundays();

        checkMyMarkers();

        DY*/
       
        
    }

    public void checkMyMarkers() {

        if (playerScript.saveMarkers[(dayNumber * 3) - 2] == 1) { ACMark.SetActive(true); HasAC = true; } else { ACMark.SetActive(false); HasAC = false; }
        if (playerScript.saveMarkers[(dayNumber * 3) - 1] == 1) { SCMark.SetActive(true); HasSC = true; } else { SCMark.SetActive(false); HasSC = false; }
        if (playerScript.saveMarkers[(dayNumber * 3) - 0] == 1) { WCMark.SetActive(true); HasWC = true; } else { WCMark.SetActive(false); HasWC = false; }
    }

    public void updateMarkers() {
        /* DY commenting out
        if (gameboardScript.newMonth == true)       // if it is a new month then
        {
            randomMarkers();                // randomize which of the child markers of this day are active
            amIValid();                     // make yourself inactive if you're not valid (DayBox) along with all your markers (children)
        }
        else
        {                              // if not a new month
            checkMyMarkers();          // check  which of your markers should be on
        }
        */
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

    public void drawCards() { drawACCard();drawSCCard();drawWCCard(); }

    public void drawACCard() {

        if (HasAC) {
            HasAC = false;
            gameboardScript.showRandomAcademic();
            ACMark.SetActive(false);
            //playerScript.saveMarkers[(dayNumber * 3) - 2] = 0;
            //return;
        }
    }

    public void drawSCCard() {

        if (HasSC) {
            HasSC = false;
            gameboardScript.showRandomSocial();
            SCMark.SetActive(false);
            //playerScript.saveMarkers[(dayNumber * 3) - 1] = 0;
            //return;
        }
    }

    public void drawWCCard()
    {
        if (HasWC)
        {
            HasWC = false;
            gameboardScript.showRandomWork();
            WCMark.SetActive(false);
            //playerScript.saveMarkers[(dayNumber * 3) - 0] = 0;
            //return;
        }
    }   

    //DY************adding method to see if day has active marker

    public bool hasActiveMarker()
    {
        return (HasAC || HasSC || HasWC);
    }

    //DY***************adding method to deactivate markers
    public void deactivateMarkers()
    {
        HasAC = false;
        HasSC = false;
        HasWC = false;
    }
}
