using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class is for the scene the game is to be played on.  It provides functions to populate the calendar, add components 
 * to the player to allow them to move from day to day, and perform checks on each day the player lands on to perform
 * necessary logic if necessary.  If a player lands on a day where a card is to be drawn, functions are provided to draw
 * cards. There are also methods for buttons on the UI to change to another scene.
 */
public class NewGBScript : MonoBehaviour {

    GameObject sceneChanger;
    GameObject player;
    GameObject summerYesNo;
    GameObject summerYesNoPanel;
    GameObject helpPanel;
    GameObject help;
    GameObject[] dayBoxes;
    GameObject[] acCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Academic Cards
    GameObject[] scCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Social Cards
    GameObject[] wcCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Work Cards

    SceneChanger sceneChangerScript;
    PlayerScript playerScript;

    int counter;
    int minutes = 0;
    int seconds = 0;
    int semesterStartDay = 10;
    int semesterEndDay = 17;
    int[] numDaysInMonth;

    string[] months;

    public Text monthText;                                      //Month display box on UI
    public Text yearText;                                       //Year display on UI    
    public Text timeElapsedText;                                //Time elapsed display on UI

    static NewGBScript newGBScript = null;

    static int startDay = 6;                                    //Day monthbegins
    static int currentDay = 17;                                 //Day the player is currently on
    static int currentMonth = 0;               
    static int currentYear = 0;

    static bool calendarShift = false;                          
    static bool playerClickTriggerAdded = false;

    // Use this for initialization
    void Start () {
        if(newGBScript == null)
        {
            newGBScript = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        newGBScript.gameObject.SetActive(true);

        summerYesNo = GameObject.Find("SummerYesNo");
        summerYesNoPanel = GameObject.Find("SummerYesNoPanel");
        help = GameObject.Find("Help");
        helpPanel = GameObject.Find("HelpPanel");

        //Hide panel displayed at end of spring semester
        summerYesNo.SetActive(false);
        summerYesNoPanel.SetActive(false);

        months = new string[]{ "September", "October", "November", "December", "January", "February", "March", "April", "May", "June", "July", "August" };
        numDaysInMonth = new int[]{ 30, 31, 30, 31, 31, 28, 31, 30, 31, 30, 31, 31 };

        initializeDayBoxes();
        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        if (calendarShift) { shiftCalendar(); }

        sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger");
        sceneChangerScript = sceneChanger.GetComponent<SceneChanger>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        if (!playerClickTriggerAdded) { addPlayerClickTrigger(); }
        player.transform.position = dayBoxes[currentDay].transform.position;
        player.GetComponent<SpriteRenderer>().sortingOrder = 0;

        //Load all cards from each category into its corresponding array
        acCardsArray = Resources.LoadAll<GameObject>("AC") as GameObject[];
        scCardsArray = Resources.LoadAll<GameObject>("SC") as GameObject[];
        wcCardsArray = Resources.LoadAll<GameObject>("WC") as GameObject[];
    }
        	
	// Update is called once per frame
	void Update () {
        counter++;

        if(Input.GetKeyDown(KeyCode.Space))
        {
                advanceDay();
        }

        /*
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(counter % 1 == 0)
            advanceDay();
        }
        */

        if(Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentYear = 4;
            currentMonth = 8;
            currentDay = 6;
            populateCalendar(3, 31);

            player.transform.position = dayBoxes[currentDay].transform.position;
        }

        //Minutes elapsed
        minutes = counter / 3600;
        //Seconds elapsed
        seconds = (counter / 60) - (minutes * 60);

        //Display elapsed time on UI
        timeElapsedText.text = "Time Elapsed\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //This method initializes the dayBoxes array with all the day gameObjects in the calender
    void initializeDayBoxes()
    {
        dayBoxes = new GameObject[37];

        for (int i = 0; i < dayBoxes.Length; i++)                                    
        {
            dayBoxes[i] = GameObject.Find("Day" + (i + 1).ToString());
            dayBoxes[i].GetComponent<TextMesh>().characterSize = 0.1f;
            dayBoxes[i].GetComponent<TextMesh>().fontSize = 50;
            dayBoxes[i].GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            dayBoxes[i].GetComponent<TextMesh>().color = Color.black;
        }
    }

    //This method number text and set card markers for each day in current momnth.
    void populateCalendar(int startDay, int numDays)
    {
        //Sets Month and Year text boxes
        monthText.text = months[currentMonth];
        yearText.text = (currentYear + 2018).ToString();

        //Check to see if month is February of a leap year. If true, number of days is set to 29. If not, number of days is set to 28
        if (currentYear % 4 != 0 && currentMonth == 5)
        {
            numDaysInMonth[5] = 28;
            numDays = numDaysInMonth[5];
        }
        else if (currentYear % 4 == 0 && currentMonth == 5)
        {
            numDaysInMonth[5] = 29;
            numDays = numDaysInMonth[5];
        }

        //Clear previous month's days from calendar
        for (int i = 0; i < dayBoxes.Length; i++)
        {
            dayBoxes[i].GetComponent<Day>().turnOffDay();
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }

        //Write current month's days to calendar in proper location
        //Any day that is not a school day is marked with an "X".
        for (int i = 0; i < numDays; i++)
        {
            if(startDay + i  < currentDay)
            {
                dayBoxes[startDay + i].GetComponent<TextMesh>().text = "X";
            }
            else if((currentMonth == 3 || currentMonth == 8 || currentMonth == 11) && i >= semesterEndDay)
            {
                dayBoxes[i].GetComponent<TextMesh>().text = "X";
                dayBoxes[i].GetComponent<Day>().turnOffDay();
            }
            else
            {
                dayBoxes[startDay + i].GetComponent<TextMesh>().text = (i + 1).ToString();
                dayBoxes[startDay + i].GetComponent<Day>().randomMarkers();
            }  
        }

        dayBoxes[currentDay].GetComponent<TextMesh>().text = "";
    }

    //This method adds an event trigger and box collider 2D to the Player so it can advance to next day if it is clicked
    void addPlayerClickTrigger()
    {
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<EventTrigger>();

        EventTrigger trigger = player.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        //Set method to be called upon triggered to advanceDay()
        entry.callback.AddListener((eventData) => { advanceDay(); });
        trigger.triggers.Add(entry);

        playerClickTriggerAdded = true;
    }

    //This method advance Player to next day where various checks are performed
    public void advanceDay()
    {
        //If the day has card markers, a card corresponding to each color marker is drawn
       if (dayBoxes[currentDay].GetComponent<Day>().hasActiveMarker())
        {
            dayBoxes[currentDay].GetComponent<Day>().drawCards();
        }

        else
        {
            //If the current day is the end of the month, the next month is loaded
            if (currentDay - startDay + 1 == numDaysInMonth[currentMonth])
            {
                loadNextMonth();
            }
            else
            {
                //Moves player to next day and increments currentDay
                player.transform.position = dayBoxes[++currentDay].transform.position;
                dayBoxes[currentDay].GetComponent<TextMesh>().text = "";
                dayBoxes[currentDay - 1].GetComponent<TextMesh>().text = "X";

                //If it is the end of the the four years of FSU, the game end and gameEnd scene is loaded
                if (currentYear == 4 && currentMonth == 8 && currentDay == 10)
                {
                    //hide player
                    Vector3 position = player.transform.position;
                    position.x += 100;
                    player.transform.position = position;
                    this.gameObject.SetActive(false);
                    SceneManager.LoadScene("gameEnd");
                }

                //If the current month is a month where the semester end falls, checkEndOfSemester() is called.
                if (currentMonth == 3 || currentMonth == 8 || currentMonth == 11)
                    checkEndofSemester();

                //Checks if it is Sunday
                checkBeginningOfWeek();
            }
        }
    }

    //This method checks if it is the end of the fall, spring, or summer semester. If so, appropriate logic is performed
    void checkEndofSemester()
    {
        if (isFallSemesterEnd())
        {
            //hide player
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            //Set calendar state to beginning of spring semester
            currentMonth++;
            currentYear++;
            startDay = (startDay + 3) % 7;
            currentDay = semesterStartDay;
            populateCalendar(startDay, numDaysInMonth[currentMonth]);

            this.gameObject.SetActive(false);
            SceneManager.LoadScene("WinterBreak");
        }
        //If it is the end of the spring semester, a panel pops up asking the player if they plan to take summer courses
        else if (isSpringSemesterEnd())
        {
            summerYesNo.SetActive(true);
            summerYesNoPanel.SetActive(true);
        }
        else if (isSummerSemesterEnd())
        {
            //hide player
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            //Set calendar state to beginning of fall semester
            currentMonth = 0;
            startDay = (startDay + 3) % 7;
            currentDay = semesterStartDay;
            populateCalendar(startDay, numDaysInMonth[currentMonth]);

            this.gameObject.SetActive(false);
            SceneManager.LoadScene("SummerBreak");
        }
    }

    //If the user elects to take summer courses, the Gameboard state is set to the beginning of the summer semester and the SummerBreak scene is loaded 
    public void yesSummerSemester()
    {
        //hide player
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        //Set gameboard state to beginning of summer semester
        currentMonth++;
        startDay = (startDay + 3) % 7;
        currentDay = semesterStartDay;
        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        GameObject.Find("SummerYesNo").SetActive(false);
        GameObject.Find("SummerYesNoPanel").SetActive(false);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("SummerBreak");
    }

    //If the user elects not to take summer courses, the Gameboard state is set to the beginning of the fall semester and the SummerBreak scene is loaded
    public void noSummerSemester()
    {
        //hide player
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        //Set calendar state to beginning of next semester
        currentMonth = 0;
        startDay = (startDay + 4) % 7;
        currentDay = semesterStartDay;
        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        //Load SummerBreak scene.
        GameObject.Find("SummerYesNo").SetActive(false);
        GameObject.Find("SummerYesNoPanel").SetActive(false);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("SummerBreak");
    }

    //This method checks if the player is on the last day of the fall semester
    bool isFallSemesterEnd()
    {
        if (currentMonth == 3 && semesterEndDay == currentDay)
            return true;

        return false;
    }

    //This method checks if the player is on the last day of the spring semester
    bool isSpringSemesterEnd()
    {
        if (currentMonth == 8 && semesterEndDay == currentDay)
            return true;

        return false;
    }

    //This method checks if the player is on the last day of the summer semester
    bool isSummerSemesterEnd()
    {
        if (currentMonth == 11 && semesterEndDay == currentDay)
            return true;

        return false;
    }

    //This method checks if the current day is Sunday.  If so, WeekSelect scene is loaded.
    void checkBeginningOfWeek()
    {
        //If it is Sunday
        if (currentDay % 7 == 0)
        {
            //If on sixth week of month, shift calendarup to display the sixth week
            if (currentDay == 35)
            {
                shiftCalendar();
            }

            player.transform.position = dayBoxes[currentDay].transform.position;

            this.gameObject.SetActive(false);
            launchWeekSelectionScreen();
        }
    }
    
    //This method shifts the calendar up to display the sixth week of a month
    void shiftCalendar()
    {
        calendarShift = true;

        //Shift calendar up one week
        Vector3 position = GameObject.Find("Month").transform.position;
        position.y += 1.55f;
        GameObject.Find("Month").transform.position = position;

        //Clear TextMesh text for first week so it doesn't interfere with the UI objects
        for (int i = 0; i < 7; i++)
        {
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }
    }
    
    //This method unshifts the calendar, hiding the sixth week.
    void unshiftCalendar()
    {
        calendarShift = false;

        //Shift calendar down one week, to its original position
        Vector3 position = GameObject.Find("Month").transform.position;
        position.y -= 1.55f;
        GameObject.Find("Month").transform.position = position;

        player.transform.position = dayBoxes[currentDay].transform.position;

        for (int i = 0; i < 7; i++)
        {
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }
    }

    //This method sets the next months attributes and loads the calendar
    void loadNextMonth()
    {
        //If current month is at the end of the months array, current month will be set to index 0;
        if (currentMonth == 11)
        {
            currentMonth = 0;
        }
        else
        {
            //Increment currentMonth
            currentMonth++;
        }

        if(calendarShift)
        {
            unshiftCalendar();
        }

        //Increment current day and set it to the corresponding day in the first week
        currentDay = (++currentDay % 7);
        startDay = currentDay;
        calendarShift = false;
        populateCalendar(startDay, numDaysInMonth[currentMonth]);
        player.transform.position = dayBoxes[currentDay].transform.position;
    }

    //This method loads the WeekSelect scene
    public void launchWeekSelectionScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("WeekSelect");
    }

    //This method loads the Status scene
    public void launchStatusScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("Status");
    }

    //This method loads the Transcript scene
    public void launchTranscriptScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("Transcript");
    }

    //This method displays the help panel for the gameboard
    public void openHelpPanel()
    {
        help.SetActive(true);
        helpPanel.SetActive(true);
    }

    //This method closes the help panel
    public void closeHelpPanel()
    {
        help.SetActive(false);
        helpPanel.SetActive(false);
    }

    //This method displays a random academic card
    public void showRandomAcademic()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(-6, -0.874f, -5);
        GameObject.Instantiate(acCardsArray[card], cardPos, this.transform.rotation);
        Debug.Log("Random Card #" + card);
    }

    //This method displays a random social card
    public void showRandomSocial()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(0, -0.874f, -5);
        GameObject.Instantiate(scCardsArray[card], cardPos, this.transform.rotation);
        Debug.Log("Random Card #" + card);
    }

    //This method displays a random social card
    public void showRandomWork()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(6, -0.874f, -5);
        GameObject.Instantiate(wcCardsArray[card], cardPos, this.transform.rotation);
         Debug.Log("Random Card #" + card);
    }
}
