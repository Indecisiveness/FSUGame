using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameboardScript : MonoBehaviour {


    GameObject sceneChanger;                                    // Object representing the sceneChanger gameObject
    SceneChanger sceneChangerScript;                            // Object representing the script attached to the scenChanger gameObject
    GameObject player;                                          // Object representing the player
    PlayerScript playerScript;

    public int currentDayBox;                                          // The current DayBox location of Player 1// Object representing the Script attached to the player
    public int currentMonth;                                           // The integer value 1-12 of the current month the Gameboard should be displaying
    int startDayBox;                                            // The integer value 1-36 of the position on the calendar the player should start at when screen loads
    public bool newMonth = true;

    GameObject[] dayBoxArray = new GameObject[43];      // An array that holds all the Day_Box objects
    

    bool acMarker;                                              // represents if the Academic card marker is active when the player enters the day
    bool wcMarker;                                              // represents if the Work card marker is active when the player enters the day 
    bool scMarker;                                              // represents if the Social card marker is active when the player enters the day

    GameObject[] acCardsArray = new GameObject[8];              // Array the stores all the Card Objects for the Academic Cards
    GameObject[] scCardsArray = new GameObject[8];              // Array the stores all the Card Objects for the Social Cards
    GameObject[] wcCardsArray = new GameObject[8];              // Array the stores all the Card Objects for the Work Cards
    
    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");                // attaching a reference to player1 to use his components
       
        playerScript = player.GetComponent<PlayerScript>();                 // attaching a reference to player1's Script to call methods and alter stats

        Debug.Log("Player1 found and referenced.");

        player.GetComponent<SpriteRenderer>().color = new Vector4(235, 235, 235, 255);          // make sure the player is visible when the GameBoard loads

        sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger");                        // attaching a reference to scenChanger to use its components
        sceneChangerScript = sceneChanger.GetComponent<SceneChanger>();                         // attaching a reference to scenChangerScript to use its methods and variables
        Debug.Log ("FoundSceneChanger");



        for (int i = 1; i < 43; i++)                                        // populate an array with all DayBox objects alive in the scene
        {
            if (i < 10) { dayBoxArray[i] = GameObject.Find("Day_0" + i.ToString()); }
            else { dayBoxArray[i] = GameObject.Find("Day_" + i.ToString()); }

            dayBoxArray[i].SetActive(true);

            Debug.Log("Day_" + i.ToString() + " referenced!");
        }

        acCardsArray = Resources.LoadAll<GameObject>("AC") as GameObject[];
        scCardsArray = Resources.LoadAll<GameObject>("SC") as GameObject[];
        wcCardsArray = Resources.LoadAll<GameObject>("WC") as GameObject[];

        if (playerScript.saveDayBox == 0 && playerScript.saveMonth == 1)                                   // pick the position of the player at initialization
        {
            Debug.Log("It says zero here dude.");
            currentMonth = 1;
            this.transform.GetChild(0).gameObject.SetActive(true);
            playerScript.saveMonth = 1;
            pickStartDayBox();
            newMonth = true;
            
        }
        else {
            Debug.Log("Loading saved position");
            currentDayBox = playerScript.saveDayBox;
            player.transform.position = dayBoxArray[currentDayBox].transform.position;            
            currentMonth = playerScript.saveMonth;
			this.transform.Find("" + currentMonth).gameObject.SetActive(true);
            player.GetComponent<SpriteRenderer>().color = new Vector4(235, 235, 235, 255);
            newMonth = false;
            
        }
			


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /** This method checks which month the Gameboard should currently be displaying, and based on that month, moves the character to the
     *  appropriate start position on the board.
     **/

    void pickStartDayBox() {

        if (currentMonth == 1) { startDayBox = 3;}
        else if (currentMonth == 2) { startDayBox = 5; }
        else if (currentMonth == 3) { startDayBox = 1; }
        else if (currentMonth == 4) { startDayBox = 3; }
        else if (currentMonth == 5) { startDayBox = 6; }
        else if (currentMonth == 6) { startDayBox = 2; }
        else if (currentMonth == 7) { startDayBox = 2; }
        else if (currentMonth == 8) { startDayBox = 4; }
        else if (currentMonth == 9) { startDayBox = 6; }
        else if (currentMonth == 10) { startDayBox = 2; }
        else if (currentMonth == 11) { startDayBox = 3; }
        else if (currentMonth == 12) { startDayBox = 6; }



		//int holdDay = currentDayBox;

		currentDayBox = startDayBox;

		player.transform.position = dayBoxArray[currentDayBox].transform.position;        
		playerScript.saveDayBox = currentDayBox;

		//if (currentMonth != 1) {
			//this.ReplaceWeek (holdDay, currentDayBox);
		//}



    }

    /** This method moves the character one day at a time, and each time checks if it is a new week or a new month.
     *  The method is called by the NEXT DAY button, which was created in the Unity Editor, so a reference was dragged in manually, not in code
     **/
    public void nextDay() {
        //Day today = dayBoxArray [currentDayBox].GetComponent<Day>();
        //if (today.HasAC || today.HasSC || today.HasWC) {
        //	today.DrawCard ();
        //}
        //else {
            newMonth = false;

		if (currentDayBox % 7 == 1 && (dayBoxArray[currentDayBox].GetComponent<Day>().HasAC||dayBoxArray[currentDayBox].GetComponent<Day>().HasSC||dayBoxArray[currentDayBox].GetComponent<Day>().HasWC)) {
			Day sunday = dayBoxArray [currentDayBox].GetComponent<Day> ();
			sunday.drawCards ();
		} else {
			currentDayBox += 1;
			player.transform.position = dayBoxArray [currentDayBox].transform.position;        
			playerScript.saveDayBox = currentDayBox;
			whichMarkers ();
			nextMonth ();
			isNewWeek ();
			for (int i = 1; i < 43; i++) {
				dayBoxArray [i].GetComponent<Day> ().amIValid ();
			}
		}
		//}
    }

    /** This method checks if it is a new month. If so, the current month is made inactive, and the following month is activated. This works
     * because all calendars occupy the same game space.
     **/
    void nextMonth() {
        if (currentMonth == 1 && currentDayBox == 33)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;
        }
        else if (currentMonth == 2 && currentDayBox == 36)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;
        }
        else if (currentMonth == 3 && currentDayBox == 31)
        {
            destroyCalendar();
            newMonth = true;
        }
        else if (currentMonth == 4 && currentDayBox == 34)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;
            launchSemesterEndScreen();
        }
        else if (currentMonth == 5 && currentDayBox == 37)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 6 && currentDayBox == 30)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 7 && currentDayBox == 32)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 8 && currentDayBox == 34)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 9 && currentDayBox == 37)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;
            launchSemesterEndScreen();
        }
        else if (currentMonth == 10 && currentDayBox == 31)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 11 && currentDayBox == 34)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;

        }
        else if (currentMonth == 12 && currentDayBox == 36)
        {
            destroyCalendar();
            playerScript.resetSaveMarkers();
            newMonth = true;
            launchSemesterEndScreen();
        }

        for (int i = 1; i < 43; i++) {

            dayBoxArray[i].GetComponent<Day>().updateMarkers();
        }
    }

    /** This method checks if it is a new week. If so, it takes the player to the weekSelect screen
     **/
    void isNewWeek() {
        if (currentDayBox == 1 || currentDayBox == 8 || currentDayBox == 15 || currentDayBox == 22 || currentDayBox == 29)
        {
			
            launchWeekSelectionScreen();

        }
    }

    
    /**
    void RollWeek(){
		int DaysLeft = 7 - ((currentDayBox) % 7);


		List<GameObject> ThisWeek = dayBoxArray.GetRange (currentDayBox, DaysLeft);

		ThisWeek.ForEach( x =>  {
			Day myDay = x.GetComponent<Day>();
			myDay.WeekStart();
		});
	}
				
	void ReplaceWeek(int LastDay, int nextDay){
		int DaysLeft = 7 - ((nextDay) % 7);

		int i = 0;

		for (;DaysLeft > 0; DaysLeft--)
		{
			Day newDay = dayBoxArray [nextDay + i].GetComponent<Day>();
			Day oldDay = dayBoxArray[LastDay+i].GetComponent<Day>();
			newDay.HasAC = oldDay.HasAC;
			newDay.HasSC = oldDay.HasSC;
			newDay.HasWC = oldDay.HasWC;

			oldDay.HasAC = false;
			oldDay.HasSC = false;
			oldDay.HasWC = false;
			newDay.UpdateMarks();
			oldDay.UpdateMarks();

			i++;
		}
		}
	*/		


    /** This method is the logic of deactivating a current calendar and activating the following one. It deactivates the nth # child of
     *  the object this script is attached to, and activates the one after it 
     **/
    void destroyCalendar() {
		this.transform.Find("" + currentMonth).gameObject.SetActive(false);
		currentMonth += 1;
		this.transform.Find("" + currentMonth).gameObject.SetActive(true);
        playerScript.saveMonth = currentMonth;
        pickStartDayBox();

        for (int i = 1; i < 43; i++)                                        // populate an array with all DayBox objects alive in the scene
        {            
            dayBoxArray[i].SetActive(true);
            dayBoxArray[i].GetComponent<Day>().amIValid();

        }



    }

    /** Changes to the Status Screen
     **/
    public void launchStatusScreen(){
        player.GetComponent<SpriteRenderer>().color = new Vector4(0,0,0,0);
        sceneChangerScript.loadScene("Status");
    }

    public void launchTranscriptScreen()
    {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        sceneChangerScript.loadScene("Transcript");
    }

    /** Changes to the WeekSelection Screen
     **/
    public void launchWeekSelectionScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        sceneChangerScript.loadScene("WeekSelect");
    }

    /** Changes to the Semester End Screen
     **/
    public void launchSemesterEndScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        sceneChangerScript.loadScene("SemesterEnd");
    }

    /** Detect which markers are on in the current DayBox
     **/

    void whichMarkers() {

        acMarker = dayBoxArray[currentDayBox].GetComponent<Day>().HasAC;
        scMarker = dayBoxArray[currentDayBox].GetComponent<Day>().HasSC;
        wcMarker = dayBoxArray[currentDayBox].GetComponent<Day>().HasWC;

        Debug.Log("AC : " + acMarker + " SC : " + scMarker + " WC : " + wcMarker);
    }

    public void showRandomAcademic() {

        int card = (int)Mathf.Floor(Random.Range((float)1,(float)8));

        Vector3 cardPos = new Vector3(0,0,-5);

        GameObject.Instantiate(acCardsArray[card],cardPos, this.transform.rotation);
            
        Debug.Log("Random Card #"+card);
    }
    public void showRandomSocial() {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(-6, 0, -5);

        GameObject.Instantiate(scCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }
    public void showRandomWork() {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(6, 0, -5);

        GameObject.Instantiate(wcCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }

    // FOR TESTING 

    bool markersActive = true;

    public void testMarkers() {

        if (markersActive)
        {
            for (int i = 1; i < 43; i++)
            {
                dayBoxArray[i].transform.GetChild(0).gameObject.SetActive(false);
                markersActive = false;
            }
        }
        else {

            for (int i = 1; i < 43; i++)
            {
                dayBoxArray[i].transform.GetChild(0).gameObject.SetActive(true);
                markersActive = true;
            }
        }
    }
}
