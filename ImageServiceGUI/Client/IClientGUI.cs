using SharedFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Client
	{
	public delegate void Updator(CommandRecievedEventArgs args);

	interface IClientGUI
		{
		bool Running();
		void Close();
		void WriteCommandToServer(CommandRecievedEventArgs argsForCommand);
		void UpdateConstantly();
		event Updator UpdateEvent;
		}
	}
