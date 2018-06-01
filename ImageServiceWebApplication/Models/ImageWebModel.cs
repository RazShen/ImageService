using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ImageServiceWebApplication.Client;
using SharedFiles;

namespace ImageServiceWebApplication.Models
	{
	public class ImageWebModel
		{
		private IClient ImageWebModelClient { get; set; }
		private String outputDir;
		private static ImageWebModel _instance;
		public static List<Student> LoadText()
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

		private ImageWebModel()
			{
			this.ImageWebModelClient = Client.Client.Instance;
			if (ImageWebModelClient.Running())
				{
				this.ImageWebModelClient.UpdateConstantly();
				this.ImageWebModelClient.UpdateEvent += ConstUpdate;
				this.SendInitRequest();
				}
			}

		public static ImageWebModel Instance
			{
			get
				{
				if (_instance != null)
					{
					return _instance;
					}
				_instance = new ImageWebModel();
				return _instance;
				}
			}
		public String GetServiceStatus()
			{
			//find service status
			if (ImageWebModelClient.Running())
				{
				return "True";
				}
			return "False";
			}

		public int GetNumOfPics()
			{
			//Get output dir path!!
			int sleepCounter = 0;
			while(this.outputDir == null && (sleepCounter < 2)) { System.Threading.Thread.Sleep(1000); sleepCounter++; }
			if (!(this.outputDir == null))
				{
				DirectoryInfo di = new DirectoryInfo(this.outputDir);
				int jpg = di.GetFiles("*.JPG", SearchOption.AllDirectories).Length;
				int png = di.GetFiles("*.PNG", SearchOption.AllDirectories).Length;
				int bmp = di.GetFiles("*.BMP", SearchOption.AllDirectories).Length;
				int gif = di.GetFiles("*.GIF", SearchOption.AllDirectories).Length;
				return jpg + png + bmp + gif;
				}
			else
				{
				return -1;
				}
			}

		private void ConstUpdate(CommandRecievedEventArgs args)
			{
			if (args != null)
				{
				switch (args.CommandID)
					{
					case (int)CommandEnum.GetConfigCommand:
						GetComponents(args);
						break;
					}
				}
			}

		private bool SendInitRequest()
			{
			try
				{
				if (!ImageWebModelClient.Running()) { return false; }
				CommandRecievedEventArgs commandReq = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
				this.ImageWebModelClient.WriteCommandToServer(commandReq);
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				return false;
				}
			return true;
			}

		private void GetComponents(CommandRecievedEventArgs responseObj)
			{
			try
				{
				this.outputDir = responseObj.Args[0];
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
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