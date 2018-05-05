using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI.Client;
using ImageServiceGUI.ViewModel;
using Newtonsoft.Json;
using SharedFiles;

namespace ImageServiceGUI.Model
	{
	class LogModel : ILogModel
		{
		private ObservableCollection<LogTuple> _logs { get; set; }
		private IClientGUI _logClient;


		public ObservableCollection<LogTuple> Logs
			{
			get { return this._logs; }
			set => throw new NotImplementedException();
			}

		public event PropertyChangedEventHandler PropertyChanged;

		public LogModel()
			{
			_logs = new ObservableCollection<LogTuple>();
			this._logClient = new ClientGUI();
			this._logClient.Start();
				if (this._logClient.Running())
				{
				//this.GetPreviousLogs();
				}
				else
				{
				Console.WriteLine("Log Client isn't connected");
				}
			}

		private void GetPreviousLogs()
			{
			CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
			CommandRecievedEventArgs result = this._logClient.WriteCommandToServer(commandRecievedEventArgs);
			try
				{
				this._logs = JsonConvert.DeserializeObject<ObservableCollection<LogTuple>>(result.Args[0]);
				}
			catch (Exception e)
				{
				Console.WriteLine("Couldn't get previous logs");
				}
			}
		}
	}
