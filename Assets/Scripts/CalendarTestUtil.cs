using System;

public class CalendarTestUtil {

	public static void Main(string[] args) {

        //create Semester object which contains Month, Week, and Day objects
        Semester semester = new Semester();

        //print out the 5 months in Semester + "monthnumber"
        for(int i = 0; i < 5; i++)
        {
            //set month name to "month "
            semester.getMonth(i).setMonthName("month ");
            //write month name + "month number"
            Console.WriteLine(semester.getMonth(i).getMonthName() + (i + 1));
        }

        //Get month object from semester
        Month month = new Month();

        //Print out the 5 weeks in the month and the 7 days nested in each week
        for(int i = 0; i < 5; i++)
        {
            //print out weeks in month.
            Console.WriteLine("\n\n\nWeek " + (i + 1) + "\n");

            //Get Week object from month
            Week week = month.getWeek(0);

            for(int j = 0; j < 7; j++)
            {
                //Print out days of week
                Console.WriteLine(week.getDay(j).getDayName() + "  WEEK " + (i + 1));
            }

        }

    }
}
