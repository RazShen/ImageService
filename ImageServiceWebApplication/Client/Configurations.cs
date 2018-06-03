using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ImageServiceWebApplication.Client
	{
	public class Configurations
		{
		private static Configurations _instance;
		public bool Exists;
		public string OutputDirectory { get; set; }
		public string SourceName { get; set; }
		public string TumbnailSize { get; set; }
        public string LogName { get; set; }
        public ObservableCollection<string> Handlers { get; set; }

		private Configurations()
			{
			this.Exists = true;
			Handlers = new ObservableCollection<string>();
			}
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