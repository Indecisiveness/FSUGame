using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class provides a Day object who has three markers in the categories, academic, social, and work.  If the Day has active markers, a card 
 * is drawn from the corresponding category.
 */
public class Day : MonoBehaviour {

	public bool HasAC = false;            // shows whether  AC marker is active
	public bool HasSC = false;            // shows whether  SC marker is active
    public bool HasWC=false;            // shows whether  WC marker is active

    public GameObject ACMark;           // Object to store AC marker reference
    public GameObject SCMark;           // Object to store SC marker reference
    public GameObject WCMark;           // Object to store WC marker reference
    public NewGBScript gameboardScript;     // object holding reference to the GameBoardScript

    // Use this for initialization
    void Start() {
        gameboardScript = GameObject.Find("FallGB").GetComponent<NewGBScript>();
    }

    // Update is called once per frame
    void Update () {
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

        if (acOn == 0) { ACMark.SetActive(true); HasAC = true; }    // set the card marker for this day active or inactive based on random number 
        else { ACMark.SetActive(false); HasAC = false; }
        if (scOn == 0) { SCMark.SetActive(true); HasSC = true; }    // set the card marker for this day active or inactive based on random number 
        else { SCMark.SetActive(false); HasSC = false; }
        if (wcOn == 0) { WCMark.SetActive(true); HasWC = true; }    // set the card marker for this day active or inactive based on random number 
        else { WCMark.SetActive(false); HasWC = false; } 
    }

    //This method turns off all card markers for the day and sets its corresponding boolean value to false
    public void turnOffDay() {
        ACMark.SetActive(false);
        SCMark.SetActive(false);
        WCMark.SetActive(false);
        HasAC = false;
        HasSC = false;
        HasWC = false;
    }

    //This method calls the draw card method for each card category
    public void drawCards() { drawACCard(); drawSCCard(); drawWCCard(); }

    //This method draws an academic card if the day contains the corresponding card marker and turn off the marker
    public void drawACCard()
    {
        if (HasAC)
        {
            HasAC = false;
            gameboardScript.showRandomAcademic();
            ACMark.SetActive(false);
        }
    }

    //This method draws a social card if the day contains the corresponding card marker and turn off the marker
    public void drawSCCard()
    {
        if (HasSC)
        {
            HasSC = false;
            gameboardScript.showRandomSocial();
            SCMark.SetActive(false);
        }
    }

    //This method draws a work card if the day contains the corresponding card marker and turn off the marker
    public void drawWCCard()
    {
        if (HasWC)
        {
            HasWC = false;
            gameboardScript.showRandomWork();
            WCMark.SetActive(false);
        }
    }   

    //This method checks to see if the day has any active card markers
    public bool hasActiveMarker()
    {
        return (HasAC || HasSC || HasWC);
    }

    public void amIValid() { }

    public void updateMarkers() { }
}
