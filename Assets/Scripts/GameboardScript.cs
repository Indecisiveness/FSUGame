using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameboardScript : MonoBehaviour {

    GameObject player;                                          // Object representing the player
    int currentMonth;                                           // The integer value 1-12 of the current month the Gameboard should be displaying
    int startDayBox;                                            // The integer value 1-36 of the position on the calendar the player should start at
    GameObject[] dayBoxArray = new GameObject[38];              // An array that holds all the Day_Box objects
    int currentDayBox;                                          // The current DayBox location of Player 1
    PlayerScript playerScript;

    // Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");            // attaching a reference to player1 to use his components
        //player.SetActive(true);                                         // player should always be active when this scene starts
        playerScript = player.GetComponent<PlayerScript>();

        Debug.Log("Player1 found and referenced.");                     
                                                     // Always set the Gameboard to month1 for September when it starts
        
                                                          
        for (int i = 1; i < 38; i++)                                    // populate an array with all DayBox objects alive in the scene
        {
            dayBoxArray[i] = GameObject.Find("Day_"+i.ToString());
            //Debug.Log("Day_" + i.ToString() + " referenced!");
        }

        if (playerScript.saveDayBox == 0)                          // pick the position of the player at initialization
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

        player.transform.position = dayBoxArray[startDayBox].transform.position;
        playerScript.saveDayBox = startDayBox;
        currentDayBox = startDayBox;
    }

    public void nextDay() {

        player.transform.position = dayBoxArray[currentDayBox + 1].transform.position;
        playerScript.saveDayBox = currentDayBox + 1;
        currentDayBox +=1;
        isNewWeek();
        nextMonth();
        
    }

    void nextMonth() {
        if (currentMonth == 1 && currentDayBox == 33) {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 2 && currentDayBox == 36)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 3 && currentDayBox == 31)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 4 && currentDayBox == 34)
        {
            destroyCalendar();
            launchSemesterEndScreen();
        }
        else if (currentMonth == 5 && currentDayBox == 37)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 6 && currentDayBox == 30)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 7 && currentDayBox == 32)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 8 && currentDayBox == 34)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 9 && currentDayBox == 37)
        {
            destroyCalendar();
            launchSemesterEndScreen();
        }
        else if (currentMonth == 10 && currentDayBox == 31)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 11 && currentDayBox == 34)
        {
            destroyCalendar();
            launchWeekSelectionScreen();
        }
        else if (currentMonth == 12 && currentDayBox == 36)
        {
            destroyCalendar();
            launchSemesterEndScreen();
        }
    }

    void isNewWeek() {
        if (currentDayBox == 8 || currentDayBox == 15 || currentDayBox == 22 || currentDayBox == 29)
        {
            launchWeekSelectionScreen();

        }
    }

    void destroyCalendar() {
        this.transform.GetChild(currentMonth-1).gameObject.SetActive(false);
        this.transform.GetChild(currentMonth).gameObject.SetActive(true);
        currentMonth += 1;
        playerScript.saveMonth = currentMonth;
        pickStartDayBox();
        
    }

    void launchStatusScreen(){
        player.GetComponent<SpriteRenderer>().color = new Vector4(0,0,0,0);
        SceneManager.LoadScene("Status");
    }

    void launchWeekSelectionScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        SceneManager.LoadScene("WeekSelect");
    }

    void launchSemesterEndScreen() {
        player.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        SceneManager.LoadScene("SemesterEnd");
    }
}
