using ImageServiceGUI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
	{
	class MainWindowModel
		{
		private IClientGUI _logClient;

		public MainWindowModel()
			{
			this._logClient = ClientGUI.Instance;
			}
		public String HasConnection
			{
			get
				{
				if (_logClient.Running())
					{
					return "True";
					}
				return "False";
				}
			set
				{
				throw new NotImplementedException();
				}
			}
			
		}
	}
