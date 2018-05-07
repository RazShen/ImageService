using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
	{
	class MainWindowViewModel
		{
		MainWindowModel mainWindow = new MainWindowModel();


		public String HasConnection
			{
			get
				{
				return mainWindow.HasConnection;
				}
			set
				{
				throw new NotImplementedException();
				}
			}

		}
	}
