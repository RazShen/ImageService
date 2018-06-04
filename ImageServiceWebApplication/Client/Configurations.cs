using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;


namespace ImageServiceWebApplication.Client
	{
	/// <summary>
	/// Configuration class holds all the basic information.
	/// </summary>
	public class Configurations
		{
		private static Configurations _instance;
		public bool Exists;
		public string OutputDirectory { get; set; }
		public string SourceName { get; set; }
		public string TumbnailSize { get; set; }
        public string LogName { get; set; }
        public ObservableCollection<string> Handlers { get; set; }

		/// <summary>
		/// Private constructor (singleton)
		/// </summary>
		private Configurations()
			{
			this.Exists = true;
			Handlers = new ObservableCollection<string>();
			}
		/// <summary>
		/// Singelton (returns the instance of the element if exists one).
		/// </summary>
		public static Configurations Instance
			{
			get
				{
				if (_instance != null)
					{
					return _instance;
					}
				_instance = new Configurations();
				return _instance;
				}
			}
		}
	}