using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class StudentsData
		{
		public static List<Student>  LoadText()
			{
			string line;
			List<Student> listOfStudents = new List<Student>();

			// Read the file and display it line by line.
			System.IO.StreamReader file = new System.IO.StreamReader(@"~/App_Data/Students_Data.txt");
			while ((line = file.ReadLine()) != null)
				{
				string[] words = line.Split(',');
				listOfStudents.Add(new Student(words[0], words[1], words[2]));
				}
			file.Close();
			return listOfStudents;
			}

		public class Student
			{
			public int ID;
			public string firstName;
			public string lastName;
			public Student(string inID, string first, string last)
				{
				ID = Convert.ToInt32(inID);
				firstName = first;
				lastName = last;
				}
			}
		}
	}