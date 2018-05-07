using ImageServiceGUI.Client;
using Newtonsoft.Json;
using SharedFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageServiceGUI.Model
	{
	class SettingsModel : IsettingsModel
		{
		//  public string OutputDirectory { get; set; }
		//  public string SourceName { get; set; }
		//  public string LogName { get; set; }
		//  public string TumbnailSize { get; set; }
		//   public ObservableCollection<string> Handlers { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		private IClientGUI _settingsClient;


		public SettingsModel()
			{
			this._settingsClient = ClientGUI.Instance;
			if (this._settingsClient.Running())
				{
				this._settingsClient.UpdateConstantly();
				this._settingsClient.UpdateEvent += Updater;
				this.sendInitRequest();
				}
			else
				{
				Console.WriteLine("Log Client isn't connected");
				}

			//this.Initialize();
			}
		protected void NotifyPropertyChanged(string name)
			{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}



		private string m_outputDirectory;
		public string OutputDirectory
			{
			get { return m_outputDirectory; }
			set
				{
				m_outputDirectory = value;
				NotifyPropertyChanged("OutputDirectory");
				}
			}
		private string m_sourceName;
		public string SourceName
			{
			get { return m_sourceName; }
			set
				{
				m_sourceName = value;
				NotifyPropertyChanged("SourceName");
				}
			}
		private string m_logName;
		public string LogName
			{
			get { return m_logName; }
			set
				{
				m_logName = value;
				NotifyPropertyChanged("LogName");
				}
			}
		private string m_tumbnailSize;
		public string TumbnailSize
			{
			get { return m_tumbnailSize; }
			set
				{
				m_tumbnailSize = value;
				NotifyPropertyChanged("TumbnailSize");
				}
			}
		/// <summary>
		/// /get and set
		/// </summary>
		public ObservableCollection<string> Handlers { get; set; }

		private void Initialize()
			{
			//CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
			this.OutputDirectory = "OutputDir";
			this.SourceName = "SourceName";
			this.LogName = "log";
			this.TumbnailSize = "sss";
			Handlers = new ObservableCollection<string>();
			//string[] handlers = string.Split(';');
			string[] handlers = { "a", "b", "c", "d" };
			foreach (string handler in handlers)
				{
				this.Handlers.Add(handler);
				}

			}

		private void Updater(CommandRecievedEventArgs args)
			{
			if (args != null)
				{
				switch (args.CommandID)
					{
					case (int)CommandEnum.GetConfigCommand:
						GetComponents(args);
						break;
					case (int)CommandEnum.CloseHandlerCommand:
						CloseHandler(args);
						break;
					}
				}
			}

		private void CloseHandler(CommandRecievedEventArgs responseObj)
			{
			if (Handlers != null && Handlers.Count > 0 && responseObj != null && responseObj.Args != null
								 && Handlers.Contains(responseObj.Args[0]))
				{
				this.Handlers.Remove(responseObj.Args[0]);
				}
			}


		private void sendInitRequest()
			{
			try
				{
				this.OutputDirectory = string.Empty;
				this.SourceName = string.Empty;
				this.LogName = string.Empty;
				this.TumbnailSize = string.Empty;
				Handlers = new ObservableCollection<string>();
				Object thisLock = new Object();
				BindingOperations.EnableCollectionSynchronization(Handlers, thisLock);
				CommandRecievedEventArgs commandReq = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
				this._settingsClient.WriteCommandToServer(commandReq);
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}
			private void GetComponents(CommandRecievedEventArgs responseObj)
			{
			try
				{
				this.OutputDirectory = responseObj.Args[0];
				this.SourceName = responseObj.Args[1];
				this.LogName = responseObj.Args[2];
				this.TumbnailSize = responseObj.Args[3];
				string[] handlers = responseObj.Args[4].Split(';');
				foreach (string handler in handlers)
					{
					this.Handlers.Add(handler);
					}
				}
			catch (Exception e)
				{
				Console.WriteLine(e.ToString());
				}
			}
		}
	}



