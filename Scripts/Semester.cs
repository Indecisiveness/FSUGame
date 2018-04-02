using System;
using System.Collections.Generic;

public class Semester {

    private List<Month> semester;
    private string semesterName;
    private int monthsInSemester = 5;


	public Semester() {
        initializeSemester();

        semesterName = "fall";
	}

    public void initializeSemester() {
        semester = new List<Month>();

        for (int i = 0; i < monthsInSemester; i++)
            semester.Add(new Month());
    }

    public Month getMonth(int idx) {
        return semester[idx];
    }

    public int getNumberOfMonthsInSemester()
    {
        return monthsInSemester;
    }
}
