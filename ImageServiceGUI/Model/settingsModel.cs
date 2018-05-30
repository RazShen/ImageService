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
    /// <summary>
    /// settings model class
    /// </summary>
	class SettingsModel : IsettingsModel
		{
		public event PropertyChangedEventHandler PropertyChanged;
		private IClientGUI _settingsClient;

        /// <summary>
        /// settings model constractor
        /// </summary>
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

			}
        /// <summary>
        /// Used for the observable collection
        /// </summary>
        /// <param name="name"></param>
		protected void NotifyPropertyChanged(string name)
			{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
        /// <summary>
        /// this function removes a handler
        /// </summary>
        /// <param name="Handler"> a given path of handler</param>
		public void RemoveHandler(String Handler)
			{
			string[] arrToSend = { Handler };
			CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandlerCommand, arrToSend, "");
			this._settingsClient.WriteCommandToServer(eventArgs);
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

		/// <summary>
		/// This method is a delegate signed to the ClientGUI updatorEvent. every time the client gets a command
		/// from the server it invokes his event and therefore this delegate.
		/// </summary>
		/// <param name="args"></param>
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
        /// <summary>
        /// this function close a handler
        /// </summary>
        /// <param name="args">given command recieve evewnt args</param>
		private void CloseHandler(CommandRecievedEventArgs args)
			{
			if (Handlers != null && Handlers.Count > 0 && args != null && args.Args != null
								 && Handlers.Contains(args.Args[0]))
				{
				this.Handlers.Remove(args.Args[0]);
				}
			}

        /// <summary>
        /// this function sent init request to the server to get the configuration
        /// </summary>
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
        /// <summary>
        /// this function gets the settings components by command reveice event args clas
        /// </summary>
        /// <param name="responseObj">a given components </param>
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



