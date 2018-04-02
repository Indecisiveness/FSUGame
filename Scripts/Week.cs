using System;
using System.Collections.Generic;

public class Week {

    private List<Day> week;
    private string[] daysOfTheWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

	public Week() {
        initializeWeek();
	}

    private void initializeWeek() {
        week = new List<Day>();

        for (int i = 0; i < daysOfTheWeek.Length ; i++) {
            week.Add(new Day(daysOfTheWeek[i]));
        }
    }

    public Day getDay(int idx) {
        return week[idx];
    }
}
