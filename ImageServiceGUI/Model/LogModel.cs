using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ImageServiceGUI.Client;
using ImageServiceGUI.ViewModel;
using Newtonsoft.Json;
using SharedFiles;

namespace ImageServiceGUI.Model
	{
    /// <summary>
    /// log model class
    /// </summary>
	class LogModel : ILogModel
		{
		private ObservableCollection<LogTuple> _logs { get; set; }
		private IClientGUI _logClient;
		public event PropertyChangedEventHandler PropertyChanged;


		public ObservableCollection<LogTuple> Logs
			{
			get { return this._logs; }
			set => throw new NotImplementedException();
			}

        /// <summary>
        /// log model constractor
        /// </summary>
		public LogModel()
			{
			_logs = new ObservableCollection<LogTuple>();
			this._logClient = ClientGUI.Instance;
				if (this._logClient.Running())
				{
				this._logClient.UpdateEvent += this.Updater;
				this.GetPreviousLogs();
				}
				else
				{
				Console.WriteLine("Log Client isn't connected");
				}
			}
        /// <summary>
        /// get previous logs
        /// </summary>
		private void GetPreviousLogs()
			{
			this._logs = new ObservableCollection<LogTuple>();
			Object thisLock = new Object();
			BindingOperations.EnableCollectionSynchronization(_logs, thisLock);
			CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
			this._logClient.WriteCommandToServer(commandRecievedEventArgs);
			}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
		private void Updater(CommandRecievedEventArgs args)
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
					default:
						break;
					}
				}
			}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
		private void SetupPreviousLogs(CommandRecievedEventArgs args)
			{
			try
				{
				ObservableCollection<LogTuple> previousLogs = JsonConvert.DeserializeObject<ObservableCollection<LogTuple>>(args.Args[0]);
				for (int i = 1; i < previousLogs.Count(); i++)
					{
					this._logs.Add(previousLogs[i]);
					}

			
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}
        /// <summary>
        /// insert a new log
        /// </summary>
        /// <param name="args"></param>
		private void InsertLog(CommandRecievedEventArgs args)
			{
			try
				{
				this._logs.Insert(0, new LogTuple { EnumType = args.Args[1], Data = args.Args[0] });
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}
		}
	}
