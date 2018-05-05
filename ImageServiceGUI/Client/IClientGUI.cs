using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Client
	{
	interface IClientGUI
		{
		bool Running();
		bool Start();
		void Close();
		CommandRecievedEventArgs WriteCommandToServer(CommandRecievedEventArgs argsForCommand);
		void UpdateConstantly();
		}
	}
