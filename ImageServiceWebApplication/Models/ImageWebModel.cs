using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ImageServiceWebApplication.Models
	{
	public class ImageWebModel
		{
		
		public static List<Student>  LoadText()
			{
			string line;
			List<Student> listOfStudents = new List<Student>();

			// Read the file and display it line by line.
			StreamReader file = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Students_Data.txt"));
			while ((line = file.ReadLine()) != null)
				{
				string[] words = line.Split(',');
				listOfStudents.Add(new Student(words[0], words[1], words[2]));
				}
			file.Close();
			return listOfStudents;
			}

		public static Boolean GetServiceStatus()
			{
			//find service status
			return true;
			}

		public static int GetNumOfPics()
			{
			//Get output dir path!!
			DirectoryInfo di = new DirectoryInfo(@"C:\Users\Raz Shenkman\Desktop\output");

			int jpg= di.GetFiles("*.JPG", SearchOption.AllDirectories).Length;
			int png = di.GetFiles("*.PNG", SearchOption.AllDirectories).Length;
			int bmp = di.GetFiles("*.BMP", SearchOption.AllDirectories).Length;
			int gif = di.GetFiles("*.GIF", SearchOption.AllDirectories).Length;

			return jpg+png+bmp+gif;
			}
		public class Student
			{
			public Student(string inID, string first, string last)
				{
				ID = Convert.ToInt32(inID);
				firstName = first;
				lastName = last;
				}
			[Required]
			[Display(Name = "ID")]
			public int ID { get; set; }
			[Required]
			[Display(Name = "First Name")]
			public string firstName { get; set; }
			[Required]
			[Display(Name = "Last Name")]
			public string lastName { get; set; }
			}

		public class ImageWebObj
			{
			public int NumOfPic { get; set; }
			public List<Student> Students { get; set; }
			public Boolean ServiceStatus { get; set; }
			public ImageWebObj(int numberOfPics, List<Student> list, Boolean stat)
				{
				NumOfPic = numberOfPics;
				Students = list;
				ServiceStatus = stat;
				}
			}
		}
	}