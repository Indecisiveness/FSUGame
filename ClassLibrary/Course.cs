using System;
using System.Collections.Generic;

namespace Application
{
	public class Course
	{
		public string courseName // 4 letters 4 numbers, unique course identifier
		{ get; set; }

		public List<string> preRequisites // get passed into constructor from .csv program that auto generates
		{ get; set; }

		public Course (string name)
		{
			courseName = name;
			preRequisites = new List<string> (0);
		}

		public Course (string name, List<string> prereqs) //Constructor
		{
			courseName = name;
            preRequisites = prereqs;
		}

		public Boolean CanTake (List<Course> taken) // Course[] taken is from the transcript
		{
			foreach (string course in preRequisites) {  // loop through prereqs

                string a = ""; // Stored courseName from each course taken
				int i = 0;
				while (a != this.courseName && i < taken.Count) { // checks prereq name vs. names in transcript
					a = taken[i].courseName;
					i++;
				}
				if (a != course) {
					return false;
				}
			}
			return true;
		}

	}
}

