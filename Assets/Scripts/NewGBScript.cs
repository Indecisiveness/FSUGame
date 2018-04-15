using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGBScript : MonoBehaviour {

    int counter;

    public bool withCards;

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
    static int startDay = 6;
    static int currentDay = 6;
    static int currentMonth = 0;
    static int currentYear = 2018;
    static bool calendarShift = false;
    public Text monthText;
    public Text yearText;
    public Text timeElapsedText;
    float startTime;

    static bool playerClickTriggerAdded = false;
    static NewGBScript newGBScript = null;

    //*******************************************************CARD STUFF******************************************************************************
    bool acMarker;                                              // represents if the Academic card marker is active when the player enters the day
    bool wcMarker;                                              // represents if the Work card marker is active when the player enters the day 
    bool scMarker;                                              // represents if the Social card marker is active when the player enters the day

    GameObject[] acCardsArray = new GameObject[7];
    GameObject[] scCardsArray = new GameObject[7];
    GameObject[] wcCardsArray = new GameObject[7];
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

        newGBScript.gameObject.SetActive(true);

        months = new string[]{ "September", "October", "November", "December", "January", "February", "March", "April", "May", "June", "July", "August" };
        numDaysInMonth = new int[]{ 30, 31, 30, 31, 31, 28, 31, 30, 31, 30, 31, 31 };
        fallSemesterEndDays = new int[] { 25, 25, 25, 25, 25 };
        springSemesterEndDays = new int[] { 25 , 25, 25, 25, 25 };
        summerSemesterEndDays = new int[] { 25, 25, 25, 25, 25 };
        fallSemesterStartDays = new int[] { 8, 8, 8, 8, 8 };
        springSemesterStartDays = new int[] { 8, 8, 8, 8, 8 };
        summerSemesterStartDays = new int[] { 8, 8, 8, 8, 8 };



        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

        sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger");
        sceneChangerScript = sceneChanger.GetComponent<SceneChanger>();

        player.GetComponent<SpriteRenderer>().sortingOrder = 1;

        if (!playerClickTriggerAdded) { addPlayerClickTrigger(); }

        initializeDayBoxes();

        populateCalendar(startDay, numDaysInMonth[currentMonth]);

        player.transform.position = dayBoxes[currentDay].transform.position;

        if (calendarShift) { shiftCalendar(); }

        acCardsArray = Resources.LoadAll<GameObject>("AC") as GameObject[]; //Card
        scCardsArray = Resources.LoadAll<GameObject>("SC") as GameObject[]; //Card
        wcCardsArray = Resources.LoadAll<GameObject>("WC") as GameObject[]; //Card

        if(withCards)
        {
            acCardsArray = Resources.LoadAll<GameObject>("AC") as GameObject[]; //Card
            scCardsArray = Resources.LoadAll<GameObject>("SC") as GameObject[]; //Card
            wcCardsArray = Resources.LoadAll<GameObject>("WC") as GameObject[]; //Card
            RollWeek(); //Card
        }
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

        timeElapsedText.text = "Time Elapsed\n" + string.Format("{0}:{1:00}", (int)counter / 3600, (int)counter / 60);

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

    void populateCalendar(int startDay, int numDays)
    {
        //Sets Month and Year text boxes
        monthText.text = months[currentMonth];
        yearText.text = currentYear.ToString();

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
            dayBoxes[i].GetComponent<TextMesh>().text = "";
        }

        //Write current month's days to calendar
        for (int i = 0; i < numDays; i++)
        {
            dayBoxes[startDay + i].GetComponent<TextMesh>().text = (i + 1).ToString();
        }
    }

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

    public void advanceDay()
    {
        if (dayBoxes[currentDay].GetComponent<TextMesh>().text == numDaysInMonth[currentMonth].ToString())
        {
                loadNextMonth();
        }
        else
        {
            player.transform.position = dayBoxes[++currentDay].transform.position;

            //**************************CARDSTUFF************************
            if (withCards)
            {
                Day today = dayBoxes[currentDay].GetComponent<Day>();
                if (today.HasAC || today.HasSC || today.HasWC)
                {
                    today.DrawCard();
                }
                else
                    whichMarkers();
            }
            //********************************************************

            if (currentMonth == 3 || currentMonth == 8 || currentMonth == 11)
                checkEndofSemester();

           
            checkBeginningOfWeek();
        }
    }

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

            if (currentYear == 2018)
                currentDay = springSemesterStartDays[0];
            if (currentYear == 2019)
                currentDay = springSemesterStartDays[1];
            if (currentYear == 2020)
                currentDay = springSemesterStartDays[2];
            if (currentYear == 2021)
                currentDay = springSemesterStartDays[3];
            if (currentYear == 2022)
                currentDay = springSemesterStartDays[4];

            populateCalendar(startDay, numDaysInMonth[currentMonth]);
            

            this.gameObject.SetActive(false);

            SceneManager.LoadScene("WinterBreak");
        }
        else if (isSpringSemesterEnd())
        {
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            currentMonth = 0;
            startDay = (startDay + 4) % 7;

            if (currentYear == 2018)
                currentDay = fallSemesterStartDays[0];
            if (currentYear == 2019)
                currentDay = fallSemesterStartDays[1];
            if (currentYear == 2020)
                currentDay = fallSemesterStartDays[2];
            if (currentYear == 2021)
                currentDay = fallSemesterStartDays[3];
            if (currentYear == 2022)
                currentDay = fallSemesterStartDays[4];

            populateCalendar(startDay, numDaysInMonth[currentMonth]);    

            this.gameObject.SetActive(false);

            SceneManager.LoadScene("SummerBreak");
        }
        else if (isSummerSemesterEnd())
        {
            Vector3 position = player.transform.position;
            position.x += 100;
            player.transform.position = position;

            currentMonth++;


            if (currentYear == 2018)
                currentDay = fallSemesterStartDays[0];
            if (currentYear == 2019)
                currentDay = fallSemesterStartDays[1];
            if (currentYear == 2020)
                currentDay = fallSemesterStartDays[2];
            if (currentYear == 2021)
                currentDay = fallSemesterStartDays[3];
            if (currentYear == 2022)
                currentDay = fallSemesterStartDays[4];

            populateCalendar(startDay, numDaysInMonth[currentMonth]);

            this.gameObject.SetActive(false);

            SceneManager.LoadScene("SummerBreak2");
        }
    }

    bool isFallSemesterEnd()
    {
        if(currentMonth != 3)
            return false;

        if (currentYear == 2018 && fallSemesterEndDays[0] == currentDay)
            return true;
        if (currentYear == 2019 && fallSemesterEndDays[1] == currentDay)
            return true;
        if (currentYear == 2020 && fallSemesterEndDays[2] == currentDay)
            return true;
        if (currentYear == 2021 && fallSemesterEndDays[3] == currentDay)
            return true;
        if (currentYear == 2022 && fallSemesterEndDays[4] == currentDay)
            return true;

        return false;

    }

    bool isSpringSemesterEnd()
    {
        if(currentMonth != 8)
            return false;

        if (currentYear == 2018 && springSemesterEndDays[0] == currentDay)
            return true;
        if (currentYear == 2019 && springSemesterEndDays[1] == currentDay)
            return true;
        if (currentYear == 2020 && springSemesterEndDays[2] == currentDay)
            return true;
        if (currentYear == 2021 && springSemesterEndDays[3] == currentDay)
            return true;
        if (currentYear == 2022 && springSemesterEndDays[4] == currentDay)
            return true;

        return false;
    }

    bool isSummerSemesterEnd()
    {
        if (currentMonth != 11)
            return false;

        if (currentYear == 2018 && summerSemesterEndDays[0] == currentDay)
            return true;
        if (currentYear == 2019 && summerSemesterEndDays[1] == currentDay)
            return true;
        if (currentYear == 2020 && summerSemesterEndDays[2] == currentDay)
            return true;
        if (currentYear == 2021 && summerSemesterEndDays[3] == currentDay)
            return true;
        if (currentYear == 2022 && summerSemesterEndDays[4] == currentDay)
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

            if(withCards)
                RollWeek(); //Card*************************

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

        player.transform.position = dayBoxes[currentDay].transform.position;

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

        /****************************************CARD STUFFF************************************************************
        int holdDay = currentDay;

        startDay = currentDay;

        player.transform.position = dayBoxArray[currentDayBox].transform.position;
        playerScript.saveDayBox = currentDayBox;

        if (currentMonth != 1)
        {
            this.ReplaceWeek(holdDay, currentDay);
        }
        //****************************************CARDSTUFF*************************************************************/

        int holdDay = currentDay + 1; //Card********************
        currentDay = (++currentDay % 7);
        startDay = currentDay;
        calendarShift = false;
        populateCalendar(startDay, numDaysInMonth[currentMonth]);
        player.transform.position = dayBoxes[currentDay].transform.position;
        this.ReplaceWeek(holdDay, currentDay); //Card*************
    }

    public void launchWeekSelectionScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        sceneChangerScript.loadScene("WeekSelect");
    }

    public void launchStatusScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        sceneChangerScript.loadScene("Status");
    }

    public void launchTranscriptScreen()
    {
        Vector3 position = player.transform.position;
        position.x += 100;
        player.transform.position = position;

        sceneChangerScript.loadScene("Transcript");
    }

    //**********************************************************CARDSTUFF************************************************************************
    //**********************************************************CARDSTUFF************************************************************************
    void whichMarkers()
    {

        acMarker = dayBoxes[currentDay].transform.GetChild(0).transform.GetChild(0).gameObject.activeSelf;
        scMarker = dayBoxes[currentDay].transform.GetChild(0).transform.GetChild(0).gameObject.activeSelf;
        wcMarker = dayBoxes[currentDay].transform.GetChild(0).transform.GetChild(0).gameObject.activeSelf;

        Debug.Log("AC : " + acMarker + ", WC : " + wcMarker + ", SC : " + scMarker);
    }

    public void showRandomAcademic()
    {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(0, 0, -5);

        GameObject.Instantiate(acCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);
    }
    public void showRandomSocial()
    {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(0, 0, -5);

        GameObject.Instantiate(scCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }
    public void showRandomWork()
    {

        int card = (int)Mathf.Floor(Random.Range((float)1, (float)8));

        Vector3 cardPos = new Vector3(0, 0, -5);

        GameObject.Instantiate(wcCardsArray[card], cardPos, this.transform.rotation);

        Debug.Log("Random Card #" + card);

    }

    void RollWeek()
    {
        int DaysLeft = 6 - ((currentDay) % 7);

        GameObject[] thisWeek = new GameObject[DaysLeft];
        
        System.Array.Copy(dayBoxes, currentDay + 1, thisWeek, 0, DaysLeft);

        for(int i = 0; i < DaysLeft; i++)
        {
            Day myDay = thisWeek[i].GetComponent<Day>();
            myDay.WeekStart();
        };
    }

    void ReplaceWeek(int LastDay, int nextDay)
    {
        int DaysLeft = 7 - ((nextDay) % 7);

        int i = 0;

        for (; DaysLeft > 0; DaysLeft--)
        {
            Day newDay = dayBoxes[nextDay + i].GetComponent<Day>();
            Day oldDay = dayBoxes[LastDay + i].GetComponent<Day>();
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
}
