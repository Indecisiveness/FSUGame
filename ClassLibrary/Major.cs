using System;
using System.Collections.Generic;


namespace Application
{
	public class Major
	{
		public List<Course> courseList //List of courses required
		{ get; set; }

		public List<GenReq> genList;

		public Major (List<Course> required, List<GenReq> genRequired)
		{
            courseList = required;

			genList = genRequired;
		}
	}
}

