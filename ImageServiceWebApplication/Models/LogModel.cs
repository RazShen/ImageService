using ImageServiceWebApplication.Client;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Models
	{
	public class LogModel
		{
		private IClient logModelClient { get; set; }
		private static LogModel _instance;
		public List<Log> logs;
		/// <summary>
		/// private constructor for the singelton
		/// </summary>
		private LogModel()
			{

			logs = new List<Log>();
			this.logModelClient = Client.Client.Instance;
			if (logModelClient.Running())
				{
				this.logModelClient.UpdateEvent += ConstUpdate;
				this.SendInitRequest();
				}
			}
		/// <summary>
		/// Get instance of the singelton.
		/// </summary>
		public static LogModel Instance
			{
			get
				{
				if (_instance != null)
					{
					return _instance;
					}
				_instance = new LogModel();
				return _instance;
				}
			}
		/// <summary>
		/// Get update from the server about the logs.
		/// </summary>
		/// <param name="args"></param>
		private void ConstUpdate(CommandRecievedEventArgs args)
			{
			if (args != null)
				{
				switch (args.CommandID)
					{
					case (int)CommandEnum.LogCommand:
						SetupPreviousLogs(args);
						break;
					case (int)CommandEnum.NewLogMessage:
						InsertLog(args);
						break;
					}

				}
			}

		/// <summary>
		/// ask the currect logs from the server
		/// </summary>
		/// <returns></returns>
		private bool SendInitRequest()
			{
			try
				{
				if (!logModelClient.Running()) { return false; }
				CommandRecievedEventArgs commandReq = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
				this.logModelClient.WriteCommandToServer(commandReq);
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				return false;
				}
			return true;
			}

		/// <summary>
		/// add the logs from the server
		/// </summary>
		/// <param name="args"></param>
		private void SetupPreviousLogs(CommandRecievedEventArgs args)
			{
			try
				{
				ObservableCollection<LogTuple> previousLogs = JsonConvert.DeserializeObject<ObservableCollection<LogTuple>>(args.Args[0]);
				for (int i = 1; i < previousLogs.Count(); i++)
					{
					this.logs.Add(new Log { Type = previousLogs[i].EnumType.ToString(), Message = previousLogs[i].Data });
					}
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}

		/// <summary>
		/// return the logs
		/// </summary>
		/// <returns></returns>
		public List<Log> GetLogs()
			{
			//int sleepCounter = 0;
			//while (sleepCounter < 2) { System.Threading.Thread.Sleep(1000); sleepCounter++; }
			return this.logs;
			}

		/// <summary>
		/// Insert a new logs from the server
		/// </summary>
		/// <param name="args"></param>
		private void InsertLog(CommandRecievedEventArgs args)
			{
			try
				{
				
				this.logs.Insert(0, new Log { Type = args.Args[1].ToString(), Message = args.Args[0] });
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}
		/// <summary>
		/// log class
		/// </summary>
		public class Log
			{
			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Type")]
			public string Type { get; set; }

			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Message")]
			public string Message { get; set; }
			}

	
		}
	}
	
