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

		protected void NotifyPropertyChanged(string name)
			{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}

		public SettingsModel()
			{
			this.Initialize();
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
			Handlers = new ObservableCollection<string>();
			this._settingsClient = new ClientGUI();
			this._settingsClient.Start();
			if (this._settingsClient.Running())
				{
				this.getComponents();
				}
			else
				{
				Console.WriteLine("Log Client isn't connected");
				}

			}


		private void getComponents()
			{
			CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
			CommandRecievedEventArgs result = this._settingsClient.WriteCommandToServer(commandRecievedEventArgs);
			try
				{
				this.OutputDirectory = JsonConvert.DeserializeObject<String>(result.Args[0]);
				this.SourceName = JsonConvert.DeserializeObject<String>(result.Args[1]);
				this.LogName = JsonConvert.DeserializeObject<String>(result.Args[2]);
				this.TumbnailSize = JsonConvert.DeserializeObject<String>(result.Args[3]);
				this.Handlers[0] = JsonConvert.DeserializeObject<String>(result.Args[4]);
				}
			catch (Exception e)
				{
				Console.WriteLine("Couldn't get settings");
				}
			}
		}	
    }



