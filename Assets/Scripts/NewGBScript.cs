using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGBScript : MonoBehaviour {

    int counter;


    GameObject sceneChanger;
    SceneChanger sceneChangerScript;


    string[] months;
    int[] fallSemesterEndDays;
    int[] springSemesterEndDays;
    int[] summerSemesterEndDays;
    int[] fallSemesterStartDays;
    int[] springSemesterStartDays;
    int[] summerSemesterStartDays;
    int[] numDaysInMonth;
    GameObject[] dayBoxes;

    PlayerScript playerScript;
    GameObject player;
    GameObject summerYesNo;
    GameObject summerYesNoPanel;
    static int startDay = 6;
    static int currentDay = 10;
    static int currentMonth = 0;
    static int currentYear = 0;
    static bool calendarShift = false;
    public Text monthText;
    public Text yearText;
    public Text timeElapsedText;
    float startTime;
    int minutes;
    int seconds;

    static bool playerClickTriggerAdded = false;
    static NewGBScript newGBScript = null;


    //*******************************************************CARDSTUFF******************************************************************************
    
    GameObject[] acCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Academic Cards
    GameObject[] scCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Social Cards
    GameObject[] wcCardsArray = new GameObject[16];              // Array the stores all the Card Objects for the Work Cards
    //***************************************************************************************************************************************************
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

        startTime = Time.time;

        minutes = 0;
        seconds = 0;

        newGBScript.gameObject.SetActive(true);

        summerYesNo = GameObject.Find("SummerYesNo");
        summerYesNoPanel = GameObject.Find("SummerYesNoPanel");

        summerYesNo.SetActive(false);
        summerYesNoPanel.SetActive(false);

        months = new string[]{ "September", "October", "November", "December", "January", "February", "March", "April", "May", "June", "July", "August" };
        numDaysInMonth = new int[]{ 30, 31, 30, 31, 31, 28, 31, 30, 31, 30, 31, 31 };
        fallSemesterEndDays = new int[] { 17, 17, 17, 17, 17 };
        springSemesterEndDays = new int[] { 10, 10, 10, 10, 10 };
        summerSemesterEndDays = new int[] { 17, 17, 17, 17, 17 };
        fallSemesterStartDays = new int[] { 10, 10, 10, 10 };
        springSemesterStartDays = new int[] { 17, 17, 17, 17, 17 };
        summerSemesterStartDays = new int[] {10, 10, 10, 10, 10 };

        sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger");
        sceneChangerScript = sceneChanger.GetComponent<SceneChanger>();

        initializeDayBoxes();

        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        if (calendarShift) { shiftCalendar(); }

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        if (!playerClickTriggerAdded) { addPlayerClickTrigger(); }
        player.transform.position = dayBoxes[currentDay].transform.position;
        player.GetComponent<SpriteRenderer>().sortingOrder = 0;



        //***************************************CARD STUFF******************************************************************
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
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(counter % 1 == 0)
            advanceDay();
        }

        minutes = counter / 3600;
        seconds = (counter / 60) - (minutes * 60);

        timeElapsedText.text = "Time Elapsed\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

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
            Day day = dayBoxes[i].GetComponent<Day>();
            day.turnOffDay();
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }

        //Write current month's days to calendar
        for (int i = 0; i < numDays; i++)
        {
            if(startDay + i  < currentDay)
                dayBoxes[startDay + i].GetComponent<TextMesh>().text = "X";
            else
                dayBoxes[startDay + i].GetComponent<TextMesh>().text = (i + 1).ToString();

            //dayBoxes[currentDay].GetComponent<TextMesh>().text = "";

            if (currentDay <= startDay + i)
                dayBoxes[startDay + i].GetComponent<Day>().randomMarkers();
           
        }

        dayBoxes[currentDay].GetComponent<TextMesh>().text = "";
    }

    //This method adds an event trigger to the Player so it can advance to next day if it is clicked
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
            if (currentDay - startDay + 1 == numDaysInMonth[currentMonth]) //dayBoxes[currentDay].GetComponent<TextMesh>().text == numDaysInMonth[currentMonth].ToString()
            {
                loadNextMonth();
            }
            else
            {
                //Moves player to next day and increments currentDay
                player.transform.position = dayBoxes[++currentDay].transform.position;
                dayBoxes[currentDay].GetComponent<TextMesh>().text = "";
                dayBoxes[currentDay - 1].GetComponent<TextMesh>().text = "X";

                if (currentYear == 4 && currentMonth == 8 && currentDay == 10)
                {
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

    //This method checks if it is the end of the fall, spring, or summer semester
    void checkEndofSemester()
    {
        if (isFallSemesterEnd())
        {
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            currentMonth++;
            currentYear++;
            startDay = (startDay + 3) % 7;
            Debug.Log("StartDay: " + startDay.ToString());

            currentDay = springSemesterStartDays[currentYear];

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
        //If it is the end of the summer semester, 
        else if (isSummerSemesterEnd())
        {
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            currentMonth = 0;
            startDay = (startDay + 3) % 7;

            currentDay = fallSemesterStartDays[currentYear];
            populateCalendar(startDay, numDaysInMonth[currentMonth]);

            this.gameObject.SetActive(false);

            SceneManager.LoadScene("SummerBreak2");
        }
    }

    public void yesSummerSemester()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        currentMonth++;
        startDay = (startDay + 3) % 7;

        currentDay = summerSemesterStartDays[currentYear];

        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        GameObject.Find("SummerYesNo").SetActive(false);
        GameObject.Find("SummerYesNoPanel").SetActive(false);

        this.gameObject.SetActive(false);

        SceneManager.LoadScene("SummerBreak");
    }

    public void noSummerSemester()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        currentMonth = 0;
        startDay = (startDay + 4) % 7;

        currentDay = fallSemesterStartDays[currentYear];

        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        GameObject.Find("SummerYesNo").SetActive(false);
        GameObject.Find("SummerYesNoPanel").SetActive(false);

        this.gameObject.SetActive(false);

        SceneManager.LoadScene("SummerBreak");
    }

    bool isFallSemesterEnd()
    {
        if (currentMonth == 3 && fallSemesterEndDays[currentYear] == currentDay)
            return true;

        return false;
    }

    bool isSpringSemesterEnd()
    {
        if (currentMonth == 8 && springSemesterEndDays[currentYear] == currentDay)
            return true;

        return false;
    }

    bool isSummerSemesterEnd()
    {
        if (currentMonth == 11 && summerSemesterEndDays[currentYear] == currentDay)
            return true;

        return false;
    }

    void checkBeginningOfWeek()
    {
        if (currentDay % 7 == 0)
        {
            if (currentDay == 35)
            {
                shiftCalendar();
            }

            player.transform.position = dayBoxes[currentDay].transform.position;

            this.gameObject.SetActive(false);

            launchWeekSelectionScreen();
        }
    }

    void shiftCalendar()
    {
        calendarShift = true;

        Vector3 position = GameObject.Find("Month").transform.position;
        position.y += 1.55f;
        GameObject.Find("Month").transform.position = position;

        //player.transform.position = dayBoxes[currentDay].transform.position;

        for (int i = 0; i < 7; i++)
        {
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }
    }

    void unshiftCalendar()
    {
        calendarShift = false;

        Vector3 position = GameObject.Find("Month").transform.position;
        position.y -= 1.55f;
        GameObject.Find("Month").transform.position = position;

        player.transform.position = dayBoxes[currentDay].transform.position;

        for (int i = 0; i < 7; i++)
        {
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }
    }

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

        currentDay = (++currentDay % 7);
        startDay = currentDay;
        calendarShift = false;
        populateCalendar(startDay, numDaysInMonth[currentMonth]);
        player.transform.position = dayBoxes[currentDay].transform.position;
    }

    public void launchWeekSelectionScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("WeekSelect");
    }

    public void launchStatusScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("Status");
    }

    public void launchTranscriptScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        this.gameObject.SetActive(false);

        sceneChangerScript.loadScene("Transcript");
    }

    
    public void showRandomAcademic()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(0, -0.874f, -5);
        GameObject.Instantiate(acCardsArray[card], cardPos, this.transform.rotation);
        Debug.Log("Random Card #" + card);
    }

    public void showRandomSocial()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(-6, -0.874f, -5);
        GameObject.Instantiate(scCardsArray[card], cardPos, this.transform.rotation);
        Debug.Log("Random Card #" + card);
    }

    public void showRandomWork()
    {
        int card = (int)Mathf.Floor(Random.Range((float)1, (float)16));
        Vector3 cardPos = new Vector3(6, -0.874f, -5);
        GameObject.Instantiate(wcCardsArray[card], cardPos, this.transform.rotation);
         Debug.Log("Random Card #" + card);
    }
}
