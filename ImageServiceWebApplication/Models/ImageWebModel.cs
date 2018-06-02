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
using ImageServiceWebApplication.Models;

namespace ImageServiceWebApplication.Models
	{
	public class ImageWebModel
		{
		private IClient ImageWebModelClient { get; set; }
		private Client.Configurations configurations;
		private static ImageWebModel _instance;
		private bool askedForConf;
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
				this.configurations = Client.Configurations.Instance;

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
			try
				{
				//Get output dir path!!
				int sleepCounter = 0;
				while (this.configurations.OutputDirectory == null && (sleepCounter < 2)) { System.Threading.Thread.Sleep(1000); sleepCounter++; }
				if (!(this.configurations.OutputDirectory == null))
					{
					DirectoryInfo di = new DirectoryInfo(this.configurations.OutputDirectory);
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
				} catch (Exception e) { return -1; }
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
				if (this.askedForConf) { return true; }
				CommandRecievedEventArgs commandReq = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
				this.ImageWebModelClient.WriteCommandToServer(commandReq);
				this.askedForConf = true;
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
				this.configurations.OutputDirectory = responseObj.Args[0];
				this.configurations.SourceName = responseObj.Args[1];
				this.configurations.TumbnailSize = responseObj.Args[3];
				string[] handlers = responseObj.Args[4].Split(';');
				foreach (string handler in handlers)
					{
					this.configurations.Handlers.Add(handler);
					}
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