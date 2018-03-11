using System;
using System.Collections.Generic;

namespace Application
{
	public class GenReq
	{

        public string reqName;

		public List<Course> availCourse; //list of courses meeting requirement

		public List<string> courseNames; //created from list of courses


		public GenReq (string req, List<Course> meetReq) //constuctor takes array of courses
		{
            reqName = req;

			availCourse = meetReq;

			int x = meetReq.Count; //find length of courseNames

			List<string> myNames = new List<string>(x);

            if (x > 0)
            {
                for (int i = 0; i < x; i++)
                {
                    myNames.Insert(i, meetReq[i].courseName);
                };

                courseNames = myNames;
            }
		}

		public bool isCourse (Course toCheck) {//Determines if a course matches a name
			foreach (string listed in courseNames) {
				if (toCheck.courseName == listed) {//Compare to each possible value
					return true;
				}
			}
			return false; //If fails to find, return false
		}

	}
}

