using System;
using System.Collections.Generic;

public class Month {

    List<Week> month;
    string monthName;
    private int weeksInMonth = 5;

	public Month() {
        initializeMonth();

        setMonthName("october");
	}

    private void initializeMonth() {
        month = new List<Week>();

        for(int i = 0; i < weeksInMonth; i++)
        {
            month.Add(new Week());
        }
    }

    public void setMonthName(string monthName)
    {
        this.monthName = monthName;
    }

    public string getMonthName()
    {
        return monthName;
    }

    public Week getWeek(int idx)
    {
        return month[idx];
    }

}
