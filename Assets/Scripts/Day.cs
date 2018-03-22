using System;
using System.Collections.Generic;

public class Day {
    
    private string dayName;
    private int dayNumber;
    private List<Card> cards;

    public Day() : this("", 0)
    {
    }

    public Day(string dayName) : this(dayName, 0)
    {
    }

    public Day(string dayName, int dayNumber)
    {
        this.dayName = dayName;
        this.dayNumber = dayNumber;

        cards = new List<Card>();
    }

    public void setDayName(string dayName)
    {
        this.dayName = dayName;
    }

    public string getDayName() {
        return dayName;
    }

    public void setdayNumber(int dayNumber) {
        this.dayNumber = dayNumber;
    }

    public int getDayNumber()
    {
        return dayNumber;
    }

    public bool getCardYesOrNo() { //what will algorithm for deciding if a card appears or not
        return false;
    }

    
}
