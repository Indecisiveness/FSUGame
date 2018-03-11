using System;
using System.Collections.Generic;

namespace Application
{
    public class Transcript
    {
        public List<Course> coursesTaken  // initializes @ zero unless we decide a student can enter with AP credit
        { get; set; }
        public List<Course> coursesRequired // needs to match major list
        { get; set; }

		public List<GenReq> genRequired//gen ed and major non-courses
		{get; set;}

        public Boolean readyForGrad // initialize to false
        { get; set; }

        public float gpa {get;set;}

        public Transcript(Major myMajor, Major genEds)
        {
            coursesRequired = myMajor.courseList;

			int x = myMajor.genList.Count + genEds.genList.Count;  //starting here, make a new list from the two existing lists

			List<GenReq> temp = new List<GenReq> (x);

			temp.AddRange(myMajor.genList);
			temp.AddRange(genEds.genList);

			genRequired = temp;

            readyForGrad = false;
            gpa = 0f;
            coursesTaken = new List<Course> { };
        }

        public void TakeCourse(Course toTake)
        {
            if (!toTake.CanTake(coursesTaken))  // checks if course can be taken, if not breaks out of method
            {
                Console.WriteLine("Pre-reqs not met");

                return;
            }
            coursesRequired.Remove(toTake);    // coursesRequired --;
            coursesTaken.Add(toTake);           // coursesTaken ++;

            int i = 0;

            bool rem = false;

           while (i < genRequired.Count)
            {
                if (genRequired[i].isCourse(toTake))
                {
                    rem = true;
                    break;
                }
                i++;
            }

            if (rem)
            {
                genRequired.RemoveAt(i);
            } 

           

			if (coursesRequired.Count == 0 && genRequired.Count == 0)
            {
                readyForGrad = true;
            }
        }

        public void ShowRemaining ()
        {
            foreach (Course courseLeft in coursesRequired)
            {
                Console.WriteLine(courseLeft.courseName);
            }
            foreach (GenReq reqLeft in genRequired)
            {
                Console.WriteLine(reqLeft.reqName);
            }
        }

        public void ChangeMajor(Major toBecome, Major genEds)
        {
            coursesRequired = toBecome.courseList;               //Change required courses to the new list
            foreach (Course needed in coursesRequired)
            {
				Course a = coursesTaken[0];
                int i = 0;
				while (needed != a && i++ < coursesTaken.Count)
                {
                    a = coursesTaken[i];
                }
                if (needed == a)
                {
                    coursesRequired.Remove(needed);
                }

            }

			int x = toBecome.genList.Count + genEds.genList.Count;  //starting here, make a new list from the two existing lists

			List<GenReq> temp = new List<GenReq> (x);

			temp.AddRange (toBecome.genList);
			temp.AddRange (genEds.genList);

			genRequired = temp;

			foreach (GenReq needed in genRequired)
			{
				bool q = false;
				int i = 0;
				while (!q && i++ < coursesTaken.Count)
				{
					q = needed.isCourse (coursesTaken [i]);
				}
				if (q){
					genRequired.Remove(needed);
				}
			}
        }
    }
}

