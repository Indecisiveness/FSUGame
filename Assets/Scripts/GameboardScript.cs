using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameboardScript : MonoBehaviour {

    GameObject player;                                          // Object representing the player
    PlayerScript playerScript;                                  // Object representing the Script attached to the player
    int currentMonth;                                           // The integer value 1-12 of the current month the Gameboard should be displaying
    int startDayBox;                                            // The integer value 1-36 of the position on the calendar the player should start at when screen loads
    GameObject[] dayBoxArray = new GameObject[38];              // An array that holds all the Day_Box objects
    int currentDayBox;                                          // The current DayBox location of Player 1

    bool acMarker;                                              // represents if the Academic card marker is active when the player enters the day
    bool wcMarker;                                              // represents if the Work card marker is active when the player enters the day 
    bool scMarker;                                              // represents if the Social card marker is active when the player enters the day

    GameObject[] acCardsArray = new GameObject[7];
    GameObject[] scCardsArray = new GameObject[7];
    GameObject[] wcCardsArray = new GameObject[7];
    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");                // attaching a reference to player1 to use his components
       
        playerScript = player.GetComponent<PlayerScript>();

        Debug.Log("Player1 found and referenced.");                     
                                                    
        
                                                          
        for (int i = 1; i < 38; i++)                                        // populate an array with all DayBox objects alive in the scene
        {
            dayBoxArray[i] = GameObject.Find("Day_"+i.ToString());
            //Debug.Log("Day_" + i.ToString() + " referenced!");
        }

        if (playerScript.saveDayBox == 0)                                   // pick the position of the player at initialization
        {
            Debug.Log("It says zero here dude.");
            currentMonth = 1;
            this.transform.GetChild(0).gameObject.SetActive(true);
            playerScript.saveMonth = 1;
            pickStartDayBox();
            
        }
        else {
            Debug.Log("Loading saved position");
            currentDayBox = playerScript.saveDayBox;
            player.transform.position = dayBoxArray[currentDayBox].transform.position;            
            currentMonth = playerScript.saveMonth;
            this.transform.GetChild(currentMonth-1).gameObject.SetActive(true);
            player.GetComponent<SpriteRenderer>().color = new Vector4(235, 235, 235, 255);
            whichMarkers();
        }

        acCardsArray = Resources.LoadAll<GameObject>("AC") as GameObject[];
        scCardsArray = Resources.LoadAll<GameObject>("SC") as GameObject[];
        wcCardsArray = Resources.LoadAll<GameObject>("WC") as GameObject[];




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

        currentDayBox = startDayBox;
        player.transform.position = dayBoxArray[currentDayBox].transform.position;        
        playerScript.saveDayBox = currentDayBox;
        
    }

    /** This method moves the character one day at a time, and each time checks if it is a new week or a new month.
     *  The method is called by the NEXT DAY button, which was created in the Unity Editor, so a reference was dragged in manually, not in code
     **/
    public void nextDay() {

        currentDayBox += 1;
        player.transform.position = dayBoxArray[currentDayBox].transform.position;        
        playerScript.saveDayBox = currentDayBox;
        whichMarkers();
        isNewWeek();
        nextMonth();
        
    }

    /** This method checks if it is a new month. If so, the current month is made inactive, and the following month is activated. This works
     * because all calendars occupy the same game space.
     **/
    void nextMonth() {
        if (currentMonth == 1 && currentDayBox == 33) {
            destroyCalendar();
           
        }
        else if (currentMonth == 2 && currentDayBox == 36)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 3 && currentDayBox == 31)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 4 && currentDayBox == 34)
        {
            destroyCalendar();
            launchSemesterEndScreen();
        }
        else if (currentMonth == 5 && currentDayBox == 37)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 6 && currentDayBox == 30)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 7 && currentDayBox == 32)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 8 && currentDayBox == 34)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 9 && currentDayBox == 37)
        {
            destroyCalendar();
            launchSemesterEndScreen();
        }
        else if (currentMonth == 10 && currentDayBox == 31)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 11 && currentDayBox == 34)
        {
            destroyCalendar();
            
        }
        else if (currentMonth == 12 && currentDayBox == 36)
        {
            destroyCalendar();
            launchSemesterEndScreen();
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

    /** This method is the logic of deactivating a current calendar and activating the following one. It deactivates the nth # child of
     *  the object this script is attached to, and activates the one after it 
     **/
    void destroyCalendar() {
        this.transform.GetChild(currentMonth-1).gameObject.SetActive(false);
        this.transform.GetChild(currentMonth).gameObject.SetActive(true);
        currentMonth += 1;
        playerScript.saveMonth = currentMonth;
        pickStartDayBox();
        
    }

    /** Changes to the Status Screen
     **/
    void launchStatusScreen(){
        player.GetComponent<SpriteRenderer>().color = new Vector4(0,0,0,0);
        SceneManager.LoadScene("Status");
    }

    /** Changes to the WeekSelection Screen
     **/
    void launchWeekSelectionScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        SceneManager.LoadScene("WeekSelect");
    }

    /** Changes to the Semester End Screen
     **/
    void launchSemesterEndScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        SceneManager.LoadScene("SemesterEnd");
    }

    /** Detect which markers are on in the current DayBox
     **/
    void whichMarkers() {

        acMarker = dayBoxArray[currentDayBox].transform.GetChild(0).gameObject.activeSelf;
        scMarker = dayBoxArray[currentDayBox].transform.GetChild(0).gameObject.activeSelf;
        wcMarker = dayBoxArray[currentDayBox].transform.GetChild(0).gameObject.activeSelf;

        Debug.Log("AC : " + acMarker + "WC : " + wcMarker + "SC : " + scMarker);
    }

    public void showRandomAcademic() {

        int card = (int)Mathf.Floor(Random.Range((float)1,(float)8));

        Vector3 cardPos = new Vector3(0,0,-5);

        GameObject.Instantiate(acCardsArray[card],cardPos, this.transform.rotation);
            
        Debug.Log("Random Card #"+card);
    }
    public void showRandomSocial() {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(0, 0, -5);

        GameObject.Instantiate(scCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }
    public void showRandomWork() {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(0, 0, -5);

        GameObject.Instantiate(wcCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }
}
